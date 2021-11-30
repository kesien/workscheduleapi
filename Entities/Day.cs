using System.ComponentModel.DataAnnotations;

namespace WorkScheduleMaker.Entities
{
    public class Day
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsWeekend { get; set; }
        public ICollection<MorningSchedule> UsersScheduledForMorning { get; set; }
        public ICollection<Forenoonschedule> UsersScheduledForForenoon { get; set; }
        public ICollection<HolidaySchedule> UsersOnHoliday { get; set; }
    }
}