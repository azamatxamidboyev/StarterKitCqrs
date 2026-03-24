using Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.User.DeleteUser
{
    public class DeleteUserCommandHandler:IRequestHandler<DeleteUserCommand,bool>
    {
        private readonly AppDbContext _context;
        public DeleteUserCommandHandler(AppDbContext context)
        {
            _context=context;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
            if (user == null) return false;
            if(user.UserRoles.Any())
            {
                _context.UserRoles.RemoveRange(user.UserRoles);
            }
            _context.Users.Remove(user);

            await _context.SaveChangesAsync(cancellationToken);
            return true;


        }
    }
}
