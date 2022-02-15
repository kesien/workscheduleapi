namespace WorkSchedule.Application.Persistency.Entities
{
    public class MorningSchedule : BaseEntity
    {
        public User User { get; set; }
        public bool IsRequest { get; set; }
        public Day Day { get; set; }
    }
}
