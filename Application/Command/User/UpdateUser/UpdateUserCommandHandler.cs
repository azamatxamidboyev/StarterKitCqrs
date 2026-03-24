using Domain.Entities;
using Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly AppDbContext _context;
        public UpdateUserCommandHandler(AppDbContext context) => _context=context;

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u=>u.UserRoles)
                .FirstOrDefaultAsync(u=>u.Id == request.Id,cancellationToken);

            if (user == null) return false;

            user.FullName = request.FullName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.DateOfBirth = request.DateOfBirth;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password); 
            }

            _context.UserRoles.RemoveRange(user.UserRoles);

            foreach (var roleName in request.Roles)
            {
                
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
                if (role != null)
                {
                    user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
