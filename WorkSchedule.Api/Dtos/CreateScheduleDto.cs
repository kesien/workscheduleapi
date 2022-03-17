namespace WorkSchedule.Api.Dtos
{
    public class CreateScheduleDto
    {
        public string UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
