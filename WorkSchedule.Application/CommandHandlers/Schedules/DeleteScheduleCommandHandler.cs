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
    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, Unit>
    {
        private readonly IScheduleService _scheduleService;
        private readonly ICustomPublisher _publisher;
        private readonly ILogger _logger;
        public DeleteScheduleCommandHandler(IScheduleService scheduleService, ICustomPublisher publisher, ILogger logger)
        {
            _scheduleService = scheduleService;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteScheduleCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validatorResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var result = await _scheduleService.DeleteSchedule(request.Id);
            if (result is null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { $"Couldn't delete schedule with id: {request.Id}" } };
            }
            _logger.Information($"Schedule for: {result.Year}-{result.Month} with ID: {result.Id} has been deleted");
            await _publisher.Publish(new ScheduleDeletedEvent(result), PublishStrategy.ParallelNoWait, cancellationToken);
            return Unit.Value;
        }
    }
}
