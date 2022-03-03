using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Hubs;
using WorkSchedule.Application.Services.EmailService;
using WorkSchedule.Application.Services.ScheduleService;

namespace WorkSchedule.Application.CommandHandlers.Schedules
{
    public class AddNewScheduleCommandHandler : IRequestHandler<AddNewScheduleCommand, Unit>
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEmailService _emailService;

        public AddNewScheduleCommandHandler(IScheduleService scheduleService, IEmailService emailService)
        {
            _scheduleService = scheduleService;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(AddNewScheduleCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddNewScheduleCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validatorResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var schedule = await _scheduleService.CheckSchedule(request.Year, request.Month);
            if (schedule)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { $"There is a schedule on date: { request.Year }-{ request.Month}" } };
            }
            var newSchedule = await _scheduleService.CreateSchedule(request.Year, request.Month);
            if (newSchedule is null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { $"Couldn't create schedule on date: {request.Year}-{request.Month}" } };
            }
            await _emailService.SendNewScheduleEmail(request.UserId, request.Year, request.Month);
            return Unit.Value;
        }
    }
}
