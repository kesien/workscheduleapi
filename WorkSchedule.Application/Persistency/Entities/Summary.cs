namespace WorkSchedule.Application.Persistency.Entities
{
    public class Summary : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Morning { get; set; }
        public int Forenoon { get; set; }
        public int Holiday { get; set; }
        public MonthlySchedule MonthlySchedule { get; set; }
    }
}