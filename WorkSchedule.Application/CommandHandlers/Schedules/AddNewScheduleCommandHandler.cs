using MediatR;
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

        public AddNewScheduleCommandHandler(IScheduleService scheduleService, ICustomPublisher publisher)
        {
            _scheduleService = scheduleService;
            _publisher = publisher;
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

            await _publisher.Publish(new NewScheduleCreatedEvent { Schedule = newSchedule, UserId = request.UserId }, PublishStrategy.ParallelNoWait, CancellationToken.None);
            return Unit.Value;
        }
    }
}
