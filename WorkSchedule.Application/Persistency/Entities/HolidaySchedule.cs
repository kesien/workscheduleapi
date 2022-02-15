namespace WorkSchedule.Application.Persistency.Entities
{
    public class HolidaySchedule : BaseEntity
    {
        public User User { get; set; }
        public Day Day { get; set; }
    }
}
