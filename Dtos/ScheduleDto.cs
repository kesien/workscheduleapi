
namespace WorkScheduleMaker.Dtos
{
    public class ScheduleDto
    {
        public Guid? Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<DayDto> Days { get; set; }
        public List<SummaryDto>? Summaries { get; set; }
        public int? NumOfWorkdays { get; set; }
        public bool? IsSaved { get; set; }
    }
}
