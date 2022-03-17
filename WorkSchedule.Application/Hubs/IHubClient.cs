namespace WorkSchedule.Application.Hubs
{
    public interface IHubClient
    {
        Task ScheduleCreatedEvent();
        Task ScheduleUpdatedEvent();
        Task ScheduleDeletedEvent();
        Task UserCreatedEvent();
        Task UserUpdatedEvent();
        Task UserDeletedEvent();
        Task RequestCreatedEvent();
        Task RequestDeletedEvent();
        Task HolidayCreatedEvent();
        Task HolidayDeletedEvent();
    }
}
