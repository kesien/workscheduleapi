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

        [HttpGet]
        public async Task<IActionResult> GetSchedule([FromQuery] int year, [FromQuery] int month) 
        {
            var schedule = await _mediator.Send(new GetScheduleByDateQuery() { Month = month, Year = year });
            return Ok(schedule);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateSchedule(CreateScheduleDto createScheduleDto)
        {
            var schedule = await _mediator.Send(
                new AddNewScheduleCommand()
                {
                    UserId = createScheduleDto.UserId,
                    Month = createScheduleDto.Month,
                    Year = createScheduleDto.Year
                });
            return Ok(schedule);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(Guid id)
        {
            await _mediator.Send(new DeleteScheduleCommand() { Id = id });
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(string id, List<DayDto> daysToUpdate)
        {
            var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var schedule = await _mediator.Send(
                new UpdateScheduleCommand()
                { 
                    Id = id,
                    Days = daysToUpdate,
                    UserId = userId
                });
            return Ok(schedule);
        }
    }
}
