using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Services;

namespace WorkScheduleMaker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidaysController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        private readonly IMapper _mapper;
        public HolidaysController(IHolidayService holidayService, IMapper mapper)
        {
            _holidayService = holidayService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHolidays()
        {
            var holidays = await _holidayService.GetAll();
            var holidaysToList = _mapper.Map<List<HolidayDto>>(holidays);
            return Ok(holidaysToList);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterByYearAndMonth([FromQuery] int year, [FromQuery] int month)
        {
            var yearToFilterBy = year == 0 ? year : DateTime.Now.Year;
            var monthToFilterBy = month == 0 ? month : DateTime.Now.Month;
            var holidays = await _holidayService.Find(holiday => holiday.Year == year && holiday.Month == month);
            var holidaysToList = _mapper.Map<List<HolidayDto>>(holidays);
            return Ok(holidaysToList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoliday(Guid id)
        {
            var result = await _holidayService.Delete(id);
            if (!result)
            {
                return BadRequest($"There's no holiday with Id: {id}");
            }
            return NoContent();
        }
    }
}