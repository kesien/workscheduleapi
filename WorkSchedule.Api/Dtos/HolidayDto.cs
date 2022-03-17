namespace WorkSchedule.Api.Dtos
{
    public class HolidayDto
    {
        public Guid? Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public bool IsFix { get; set; }
    }
}