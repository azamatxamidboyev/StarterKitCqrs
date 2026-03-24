using Application.DTO;
using Application.Services;
using Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Auth.RefreshToken
{
    public class RefreshTokenUserCommandHandler : IRequestHandler<RefreshTokenUserCommand, RefreshTokenDto>
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        public RefreshTokenUserCommandHandler(AppDbContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RefreshTokenDto> Handle(RefreshTokenUserCommand request, CancellationToken cancellationToken)
        {

            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(request.dto.AccessToken);

            if (principal == null)
                throw new UnauthorizedAccessException("Yaroqsiz Access Token.");



            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("Token ma'lumotlari noto'g'ri.");



            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(userIdClaim), cancellationToken);


            if (user == null ||
                user.RefreshToken != request.dto.RefreshToken ||
                user.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh Token xato yoki muddati o'tgan.");
            }


            var roles = user.UserRoles
                .Where(ur => ur.Role != null)
                .Select(ur => ur.Role!.Name)
                .ToList();



            var tokenResponse = await _jwtTokenService.GenerateToken(user, roles);


            return new RefreshTokenDto
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken
            };
        }
    }
}

