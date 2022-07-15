using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Api.Queries.Schedules;
using WorkSchedule.Application.Hubs;

namespace Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ScheduleHub, IHubClient> _hubContext;
        public SchedulesController(IMediator mediator, IHubContext<ScheduleHub, IHubClient> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpGet("{year}/{month}")]
        public async Task<IActionResult> GetSchedule(int year, int month)
        {
            var schedule = await _mediator.Send(new GetScheduleByDateQuery() { Month = month, Year = year });
            return Ok(schedule);
        }

        [Authorize(Roles = "Superadmin,Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] AddNewScheduleCommand addNewScheduleCommand)
        {
            await _mediator.Send(addNewScheduleCommand);
            return Ok();
        }

        [Authorize(Roles = "Superadmin,Administrator")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSchedule([FromBody] DeleteScheduleCommand deleteCommand)
        {
            await _mediator.Send(deleteCommand);
            return NoContent();
        }

        [Authorize(Roles = "Superadmin,Administrator")]
        [HttpPut]
        public async Task<IActionResult> UpdateSchedule([FromBody] UpdateScheduleCommand updateCommand)
        {
            await _mediator.Send(updateCommand);
            await _hubContext.Clients.All.ScheduleUpdatedEvent();
            return Ok();
        }
    }
}
