using MediatR;
using Serilog;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Application.Constants;
using WorkSchedule.Application.Events;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Helpers;
using WorkSchedule.Application.Services.EmailService;
using WorkSchedule.Application.Services.ScheduleService;

namespace WorkSchedule.Application.CommandHandlers.Schedules
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, Unit>
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        private readonly ICustomPublisher _publisher;
        public UpdateScheduleCommandHandler(IScheduleService scheduleService, IEmailService emailService, ILogger logger, ICustomPublisher publisher)
        {
            _scheduleService = scheduleService;
            _emailService = emailService;
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<Unit> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
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
            _logger.Information($"Schedule for: {result.Year}-{result.Month} with ID: {result.Id} has been updated!");
            await _publisher.Publish(new ScheduleUpdatedEvent { Schedule = result, UserId = request.UserId }, PublishStrategy.ParallelNoWait, CancellationToken.None);
            //await _emailService.SendScheduleModifiedEmail(request.UserId.ToString(), request.Days[0].Date.Year, request.Days[0].Date.Month);
            return Unit.Value;
        }
    }
}
