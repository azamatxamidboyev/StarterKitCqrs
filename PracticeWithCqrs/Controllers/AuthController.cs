using Application.Command.Auth.Login;
using Application.Command.Auth.RefreshToken;
using Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PracticeWithCqrs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpPost]
        public async Task<ActionResult<TokenResponseDto>> Login([FromBody] LoginDto loginDto) 
        {
            
            var result = await _mediator.Send(new LoginUserCommand { Dto = loginDto });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RefreshTokenDto>> RefreshToken([FromBody] RefreshTokenDto refreshDto)
        {
            var result = await _mediator.Send(new RefreshTokenUserCommand { dto = refreshDto });
            return Ok(result);
        }

    }
}
