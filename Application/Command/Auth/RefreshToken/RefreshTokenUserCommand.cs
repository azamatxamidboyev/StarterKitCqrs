using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Auth.RefreshToken
{
    public class RefreshTokenUserCommand:IRequest<RefreshTokenDto>
    {
        public RefreshTokenDto dto;
    }
}
