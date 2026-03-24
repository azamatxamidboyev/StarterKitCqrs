using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.User.DeleteUser
{
    public class DeleteUserCommand :IRequest<bool>
    {
        public int Id { get; set; }
    }
}
