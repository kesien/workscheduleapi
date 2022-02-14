namespace WorkSchedule.Application.Helpers
{
    public class EmailClientSettings
    {
        public string? ApiKey { get; set; }
        public string? FromEmail { get; set; }
        public string? FromName { get; set; }
        public string? NewScheduleTemplateId { get; set; }
        public string? ScheduleModifiedTemplateId { get; set; }
    }
}
