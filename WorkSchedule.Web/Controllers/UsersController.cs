using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkSchedule.Api.Commands.Users;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Api.Queries.Users;

namespace Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = "";
            if (identity != null)
            {
                userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            var user = await _mediator.Send(new GetUserByIdQuery() { Id = id, RequesterId = userId });
            return Ok(user);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AddNewUserCommand addNewUserCommand)
        {
            var newUser = await _mediator.Send(addNewUserCommand);
            return Ok(newUser);
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
        {
            await _mediator.Send(updateUserCommand);
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand deleteUserCommand)
        {
            await _mediator.Send(deleteUserCommand);
            return NoContent();
        }
    }
}
