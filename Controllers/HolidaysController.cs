using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleMaker.Services;

namespace WorkScheduleMaker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidaysController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        public HolidaysController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHolidays()
        {
            var holidays = await _holidayService.GetAll();
            if (holidays is null)
            {
                return BadRequest();
            }
            return Ok(holidays);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoliday(Guid id)
        {
            var result = await _holidayService.Delete(id);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}