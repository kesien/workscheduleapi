namespace WorkSchedule.Application.Persistency.Entities
{
    public class Summary : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Morning { get; set; }
        public int Forenoon { get; set; }
        public int Holiday { get; set; }
        public MonthlySchedule MonthlySchedule { get; set; }
    }
}