using Microsoft.AspNetCore.Identity;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<MorningSchedule> MorningSchedules { get; set; }
        public ICollection<Forenoonschedule> ForenoonSchedules { get; set; }
        public ICollection<HolidaySchedule> HolidaySchedules { get; set; }
        public UserRole Role { get; set; }
    }
}
