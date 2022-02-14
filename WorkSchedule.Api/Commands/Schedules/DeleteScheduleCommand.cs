using MediatR;

namespace WorkSchedule.Api.Commands.Schedules
{
    public class DeleteScheduleCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
