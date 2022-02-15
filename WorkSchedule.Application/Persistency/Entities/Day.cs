using System.ComponentModel.DataAnnotations;

namespace WorkSchedule.Application.Persistency.Entities
{
    public class Day : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsWeekend { get; set; }
        public ICollection<MorningSchedule> UsersScheduledForMorning { get; set; }
        public ICollection<Forenoonschedule> UsersScheduledForForenoon { get; set; }
        public ICollection<HolidaySchedule> UsersOnHoliday { get; set; }
        public MonthlySchedule MonthlySchedule { get; set; }
    }
}