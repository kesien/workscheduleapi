using MediatR;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Events
{
    public class NewScheduleCreatedEvent : INotification
    {
        public MonthlySchedule Schedule { get; set; }
        public Guid UserId { get; set; }
    }
}
