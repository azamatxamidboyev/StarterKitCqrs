using Domain.Entities;
using Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.User.CreateUser
{
    public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateUserCommandHandler(
            AppDbContext context
            )
        {
             _context = context;   
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User
            {
               FullName= request.FullName,
               UserName= request.UserName,
               DateOfBirth= request.DateOfBirth,
               PhoneNumber= request.PhoneNumber,
               Password= BCrypt.Net.BCrypt.HashPassword(request.Password),
               Email=request.Email

            };
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var role in request.Roles)
            {
                var roleEntity = await _context.Roles.FirstOrDefaultAsync(x => x.Name.Equals(role));
                if (roleEntity == null)
                    continue;

                var userRole = new UserRole
                {
                    Id = 0,
                    UserId = user.Id,
                    RoleId = roleEntity.Id
                };

                await _context.AddAsync(userRole);

            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return user.Id;

        }
    }
}
