namespace WorkSchedule.Application.Persistency.Entities
{
    public class Forenoonschedule : IBaseEntity
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public bool IsRequest { get; set; }
        public Day Day { get; set; }
    }
}
