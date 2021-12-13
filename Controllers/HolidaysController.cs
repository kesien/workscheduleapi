using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;
using WorkScheduleMaker.Services;

namespace WorkScheduleMaker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
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
            var holidaysToList = _mapper.Map<IEnumerable<HolidayDto>>(holidays);
            return Ok(holidaysToList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewHoliday(HolidayDto holidayDto)
        {
            var result = await _holidayService.Add(holidayDto);
            if (result is null)
            {
                return BadRequest($"Holiday for {holidayDto.Month}-{holidayDto.Day} already exists!");
            }
            var holidayToReturn = _mapper.Map<HolidayDto>(result);
            return Ok(holidayToReturn);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterByYearAndMonth([FromQuery] int year, [FromQuery] int month)
        {
            var yearToFilterBy = year == 0 ? DateTime.Now.Year : year;
            Expression<Func<Holiday, bool>> filter = holiday => (holiday.Year == yearToFilterBy || holiday.IsFix) && holiday.Month == month;
            if (month == 0) 
            {
                filter = holiday => holiday.Year == yearToFilterBy || holiday.IsFix;
            }
            var holidays = await _holidayService.Find(filter);
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