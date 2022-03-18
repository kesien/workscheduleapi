namespace WorkSchedule.Api.Dtos
{
    public class SummaryDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Morning { get; set; }
        public int Forenoon { get; set; }
        public int Holiday { get; set; }
    }
}