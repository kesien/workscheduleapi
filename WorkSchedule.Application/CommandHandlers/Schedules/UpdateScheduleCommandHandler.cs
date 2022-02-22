using AutoMapper;
using MediatR;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Exceptions;
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
            var validator = new UpdateScheduleCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validatorResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var result = await _scheduleService.UpdateSchedule(request.Id, request.Days);
            result.Days = result.Days.OrderBy(day => day.Date).ToList();
            if (result is null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { $"Couldn't update schedule with Id: {request.Id}" } };
            }
            await _emailService.SendScheduleModifiedEmail(request.UserId.ToString(), request.Days[0].Date.Year, request.Days[0].Date.Month);
            return _mapper.Map<ScheduleDto>(result);
        }
    }
}
