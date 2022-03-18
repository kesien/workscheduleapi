namespace WorkSchedule.Application.Persistency.Entities
{
    public class HolidaySchedule : IBaseEntity
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Day Day { get; set; }
    }
}
