using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Api.Queries.Holidays;
using WorkSchedule.Application.Hubs;

namespace Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Superadmin,Administrator")]
    [ApiController]
    [Route("api/[controller]")]
    public class HolidaysController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ScheduleHub, IHubClient> _hubContext;
        public HolidaysController(IMediator mediator, IHubContext<ScheduleHub, IHubClient> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHolidays()
        {
            var holidays = await _mediator.Send(new GetAllHolidaysQuery());
            return Ok(holidays);
        }

        [HttpGet("years")]
        public async Task<IActionResult> GetAllYears()
        {
            var years = await _mediator.Send(new GetYearsForAllHolidaysQuery());
            return Ok(years);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewHoliday([FromBody] AddNewHolidayCommand command)
        {
            await _mediator.Send(command);
            await _hubContext.Clients.All.HolidayCreatedEvent();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHoliday([FromBody] DeleteHolidayCommand deleteCommand)
        {
            await _mediator.Send(deleteCommand);
            await _hubContext.Clients.All.HolidayDeletedEvent();
            return NoContent();
        }
    }
}