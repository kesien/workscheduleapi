using MediatR;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Application.Services.ScheduleService;

namespace WorkSchedule.Application.CommandHandlers.Schedules
{
    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, Unit>
    {
        private readonly IScheduleService _scheduleService;

        public DeleteScheduleCommandHandler(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task<Unit> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            var result = await _scheduleService.DeleteSchedule(request.Id);
            if (!result)
            {
                throw new ApplicationException($"Couldn't delete schedule with id: {request.Id}");
            }
            return Unit.Value;
        }
    }
}
