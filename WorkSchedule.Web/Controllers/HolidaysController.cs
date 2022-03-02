using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Api.Queries.Holidays;

namespace Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    [ApiController]
    [Route("api/[controller]")]
    public class HolidaysController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HolidaysController(IMediator mediator)
        {
            _mediator = mediator;
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
            var holiday = await _mediator.Send(command);
            return Ok(holiday);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHoliday([FromBody] DeleteHolidayCommand deleteCommand)
        {
            await _mediator.Send(deleteCommand);
            return NoContent();
        }
    }
}