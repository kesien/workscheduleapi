using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WorkSchedule.Api.Commands.Requests;
using WorkSchedule.Api.Queries.Requests;
using WorkSchedule.Application.Hubs;

namespace Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ScheduleHub, IHubClient> _hubContext;
        public RequestsController(IMediator mediator, IHubContext<ScheduleHub, IHubClient> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
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
            await _mediator.Send(command);
            await _hubContext.Clients.All.RequestCreatedEvent();
            return Ok();
        }

        [HttpGet("years/{userId}")]
        public async Task<IActionResult> GetAllRequestYearsForUser(Guid userId)
        {
            var years = await _mediator.Send(new GetAllRequestYearsForUserQuery() { UserId = userId });
            return Ok(years);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllRequestsForUser(Guid userId)
        {
            var requests = await _mediator.Send(
                new GetAllRequestsForUserQuery
                {
                    UserId = userId
                });
            return Ok(requests);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRequest([FromBody] DeleteRequestCommand deleteCommand)
        {
            await _mediator.Send(deleteCommand);
            await _hubContext.Clients.All.RequestDeletedEvent();
            return NoContent();
        }
    }
}
