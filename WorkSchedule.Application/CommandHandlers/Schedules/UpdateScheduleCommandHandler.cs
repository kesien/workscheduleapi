using AutoMapper;
using MediatR;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Services.EmailService;
using WorkSchedule.Application.Services.ScheduleService;

namespace WorkSchedule.Application.CommandHandlers.Schedules
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, ScheduleDto>
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UpdateScheduleCommandHandler(IScheduleService scheduleService, IEmailService emailService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<ScheduleDto> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var result = await _scheduleService.UpdateSchedule(request.Id, request.Days);
            if (result is null)
            {
                throw new ApplicationException($"Couldn't update schedule with Id: {request.Id}");
            }
            await _emailService.SendScheduleModifiedEmail(request.UserId, request.Days[0].Date.Year, request.Days[0].Date.Month);
            return _mapper.Map<ScheduleDto>(result);
        }
    }
}
