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
        public async Task<IActionResult> CreateUser(UserForRegisterDto userForRegisterDto)
        {
            var newUser = await _mediator.Send(
                new AddNewUserCommand()
                {
                    Name = userForRegisterDto.Name,
                    Password = userForRegisterDto.Password,
                    Role = userForRegisterDto.Role,
                    Username = userForRegisterDto.UserName
                });
            return Ok(newUser);
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserForUpdateDto userForUpdateDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = "";
            if (identity != null)
            {
                userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            await _mediator.Send(
                new UpdateUserCommand(userId, id)
                {
                    Name = userForUpdateDto.Name,
                    Password = userForUpdateDto.Password,
                    Role = userForUpdateDto.Role,
                    Username = userForUpdateDto.UserName
                });
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _mediator.Send(new DeleteUserCommand() { Id = id });
            return NoContent();
        }
    }
}
