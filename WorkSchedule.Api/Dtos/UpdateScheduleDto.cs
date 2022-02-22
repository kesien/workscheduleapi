namespace WorkSchedule.Api.Dtos
{
    public class UpdateScheduleDto
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<DayDto> Days { get; set; }
    }
}