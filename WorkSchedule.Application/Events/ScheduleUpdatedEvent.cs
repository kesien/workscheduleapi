using MediatR;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Events
{
    public class ScheduleUpdatedEvent : INotification
    {
        public MonthlySchedule Schedule { get; set; }
        public string UserId { get; set; }
    }
}
