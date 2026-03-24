using Application.Command.User.CreateUser;
using Application.Command.User.DeleteUser;
using Application.Command.User.UpdateUser;
using Application.Common.Models;
using Application.Dto;
using Application.Queries.User.GetAllUser;
using Application.Queries.User.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PracticeWithCqrs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;

        //all
        [HttpGet]
        public async Task<ActionResult<PaginatedList<UserDto>>> GetAll([FromQuery] GetAllUserQuery query)
        { 
            return await _mediator.Send(query);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (result == null)
                return NotFound(new { message = "Foydalanuvchi topilmadi." });

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateUserCommand command)
        {
          
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = userId }, userId);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] UpdateUserCommand command)
        {
            
            if (id != command.Id)
                return BadRequest(new { message = "ID mos kelmadi." });

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return NoContent(); 
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
           
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });

            if (!result)
                return NotFound();

            return Ok(new { message = "Foydalanuvchi o'chirildi." });
        }
    }
   
}
