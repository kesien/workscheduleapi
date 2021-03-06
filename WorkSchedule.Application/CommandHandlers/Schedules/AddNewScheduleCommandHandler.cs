using MediatR;
using Serilog;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Application.Constants;
using WorkSchedule.Application.Events;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Helpers;
using WorkSchedule.Application.Services.ScheduleService;

namespace WorkSchedule.Application.CommandHandlers.Schedules
{
    public class AddNewScheduleCommandHandler : IRequestHandler<AddNewScheduleCommand, Unit>
    {
        private readonly IScheduleService _scheduleService;
        private readonly ICustomPublisher _publisher;
        private readonly ILogger _logger;
        public AddNewScheduleCommandHandler(IScheduleService scheduleService, ICustomPublisher publisher, ILogger logger)
        {
            _scheduleService = scheduleService;
            _publisher = publisher;
            _logger = logger;
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

            _logger.Information($"Schedule for: {newSchedule.Year}-{newSchedule.Month} with ID: {newSchedule.Id} has been created!");
            await _publisher.Publish(new NewScheduleCreatedEvent { Schedule = newSchedule, UserId = request.UserId }, PublishStrategy.Async, CancellationToken.None);
            return Unit.Value;
        }
    }
}
