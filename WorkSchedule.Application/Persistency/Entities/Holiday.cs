namespace WorkSchedule.Application.Persistency.Entities
{
    public class Holiday : IBaseEntity
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public bool IsFix { get; set; }
    }
}