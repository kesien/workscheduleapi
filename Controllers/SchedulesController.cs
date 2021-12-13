using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleMaker.Data;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;
using WorkScheduleMaker.Services;

namespace WorkScheduleMaker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;

        public SchedulesController(IScheduleService service, IMapper mapper)
        {
            _scheduleService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedule([FromQuery] int year, [FromQuery] int month) 
        {
            var schedule = await _scheduleService.GetSchedule(year, month);
            var scheduleDto = _mapper.Map<ScheduleDto>(schedule);
            scheduleDto.Days = scheduleDto.Days.OrderBy(day => day.Date).ToList();
            return Ok(scheduleDto);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateSchedule(CreateScheduleDto createScheduleDto)
        {
            var schedule = await _scheduleService.CheckSchedule(createScheduleDto.Year, createScheduleDto.Month);
            if (schedule)
            {
                return BadRequest($"There is a schedule on date: {createScheduleDto.Year}-{createScheduleDto.Month}");
            }
            var newSchedule = await _scheduleService.CreateSchedule(createScheduleDto.Year, createScheduleDto.Month);

            if (newSchedule is null) 
            {
                return BadRequest($"Couldn't create schedule on date: {createScheduleDto.Year}-{createScheduleDto.Month}");
            }
            var scheduleDto = _mapper.Map<ScheduleDto>(newSchedule);
            scheduleDto.Days = scheduleDto.Days.OrderBy(day => day.Date).ToList();
            return Ok(scheduleDto);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(Guid id)
        {
            var result = await _scheduleService.DeleteSchedule(id);
            if (!result)
            {
                return BadRequest($"Couldn't delete schedule with Id: {id}");
            }
            return NoContent();
        }


        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(Guid id, List<DayDto> daysToUpdate)
        {
            var result = await _scheduleService.UpdateSchedule(id, daysToUpdate);
            if (result is null)
            {
                BadRequest();
            }
            var scheduleDto = _mapper.Map<ScheduleDto>(result);
            return Ok(scheduleDto);
        }
    }
}
