namespace WorkSchedule.Api.Dtos
{
    public class DayDto
    {
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsWeekend { get; set; }
        public List<UserToRequestDto> UsersScheduledForMorning { get; set; }
        public List<UserToRequestDto> UsersScheduledForForenoon { get; set; }
        public List<UserToRequestDto> UsersOnHoliday { get; set; }
    }
}
