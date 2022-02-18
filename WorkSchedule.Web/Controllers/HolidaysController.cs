using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Api.Dtos;
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

        [HttpPost]
        public async Task<IActionResult> CreateNewHoliday(HolidayDto holidayDto)
        {
            var holiday = await _mediator.Send(
                new AddNewHolidayCommand() 
                { 
                    Day = holidayDto.Day,
                    IsFix = holidayDto.IsFix,
                    Month = holidayDto.Month,
                    Year = holidayDto.Year
                });
            return Ok(holiday);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterByYearAndMonth([FromQuery] int year, [FromQuery] int month)
        {
            var holidays = await _mediator.Send(new GetHolidaysByYearAndMonthQuery() { Month = month, Year = year });
            return Ok(holidays);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoliday(Guid id)
        {
            await _mediator.Send(new DeleteHolidayCommand() { Id = id });
            return NoContent();
        }
    }
}