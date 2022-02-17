using MediatR;

namespace WorkSchedule.Api.Commands.Schedules
{
    public class DeleteScheduleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
