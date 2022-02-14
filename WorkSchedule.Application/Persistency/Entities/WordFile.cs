namespace WorkSchedule.Application.Persistency.Entities
{
    public class WordFile : BaseEntity
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public Guid MonthlyScheduleId { get; set; }
        public MonthlySchedule MonthlySchedule { get; set; }
    }
}