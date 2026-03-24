using Application.DTO;
using Application.Services;
using Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Auth.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponseDto>
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUserCommandHandler(AppDbContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<TokenResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName == dto.Login, cancellationToken);

            if (user == null)
                throw new UnauthorizedAccessException("Username yoki parol noto'g'ri.");

            
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)) 
                throw new UnauthorizedAccessException("Username yoki parol noto'g'ri.");

            
            var roles = user.UserRoles
                .Where(ur => ur.Role != null)
                .Select(ur => ur.Role!.Name)
                .ToList();

          
            var tokenResponse = await _jwtTokenService.GenerateToken(user, roles);

            
            return tokenResponse;
        }
    }
}
