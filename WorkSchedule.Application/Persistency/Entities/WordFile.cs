namespace WorkSchedule.Application.Persistency.Entities
{
    public class WordFile : IBaseEntity
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public Guid MonthlyScheduleId { get; set; }
        public MonthlySchedule MonthlySchedule { get; set; }
    }
}