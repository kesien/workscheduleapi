using MediatR;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Events
{
    public class ScheduleDeletedEvent : INotification
    {
        private readonly MonthlySchedule _schedule;
        public MonthlySchedule Schedule
        {
            get { return _schedule; }
        }

        public ScheduleDeletedEvent(MonthlySchedule schedule)
        {
            _schedule = schedule;
        }
    }
}
