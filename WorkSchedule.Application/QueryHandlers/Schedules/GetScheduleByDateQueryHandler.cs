using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Schedules;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Services.ScheduleService;

namespace WorkSchedule.Application.QueryHandlers.Schedules
{
    public class GetScheduleByDateQueryHandler : IRequestHandler<GetScheduleByDateQuery, ScheduleDto>
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;

        public GetScheduleByDateQueryHandler(IScheduleService scheduleService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        public async Task<ScheduleDto> Handle(GetScheduleByDateQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _scheduleService.GetSchedule(request.Year, request.Month);
            var scheduleDto = _mapper.Map<ScheduleDto>(schedule);
            scheduleDto.Days = scheduleDto.Days.OrderBy(day => day.Date).ToList();
            return scheduleDto;
        }
    }
}
