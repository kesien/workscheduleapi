using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Api.Queries.Schedules;

namespace Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SchedulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{year}/{month}")]
        public async Task<IActionResult> GetSchedule(int year, int month) 
        {
            var schedule = await _mediator.Send(new GetScheduleByDateQuery() { Month = month, Year = year });
            return Ok(schedule);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] AddNewScheduleCommand addNewScheduleCommand)
        {
            var schedule = await _mediator.Send(addNewScheduleCommand);
            return Ok(schedule);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSchedule([FromBody] DeleteScheduleCommand deleteCommand)
        {
            await _mediator.Send(deleteCommand);
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<IActionResult> UpdateSchedule([FromBody] UpdateScheduleCommand updateCommand)
        {
            var schedule = await _mediator.Send(updateCommand);
            return Ok(schedule);
        }
    }
}
