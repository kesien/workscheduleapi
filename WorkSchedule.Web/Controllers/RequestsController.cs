using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkSchedule.Api.Commands.Requests;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Api.Queries.Requests;

namespace Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllRequestsForAllUsers()
        {
            var requests = await _mediator.Send(new GetAllRequestsQuery());
            return Ok(requests);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] AddNewRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllRequestsForUser(string userId)
        {
            var requests = await _mediator.Send(new GetAllRequestsForUserQuery() { UserId = userId });
            return Ok(requests);
        }

        [HttpGet("{userId}/{year}/{month}")]
        public async Task<IActionResult> GetAllRequestsForUserByDate(Guid userId, int year, int month)
        {
            var requests = await _mediator.Send(
                new GetAllRequestsForUserByDateQuery()
                {
                    Month = month,
                    Year = year,
                    UserId = userId
                });
            return Ok(requests);
        }

        [HttpDelete("{requestId}")]
        public async Task<IActionResult> DeleteRequest(string requestId)
        {
            var result = await _mediator.Send(new DeleteRequestCommand() { Id = requestId });
            return NoContent();
        }
    }
}
