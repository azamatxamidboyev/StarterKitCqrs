using Application.Common.Models;
using Application.Dto;
using Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.User.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, PaginatedList<UserDto>>
    {
        private readonly AppDbContext _context;
        public GetAllUserQueryHandler(AppDbContext context)=>_context=context;
        

        public async Task<PaginatedList<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users
                        .AsNoTracking()
                        .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                        .OrderBy(u => u.FullName);

          
            var count = await query.CountAsync(cancellationToken);

            
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
                })
                .ToListAsync(cancellationToken);

            
            return new PaginatedList<UserDto>(items, count, request.PageNumber, request.PageSize);
        }
    }
}
