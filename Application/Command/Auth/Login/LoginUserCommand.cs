using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Auth.Login
{
    public class LoginUserCommand : IRequest<TokenResponseDto>
    {
        public LoginDto Dto { get; set; }
    }
}
