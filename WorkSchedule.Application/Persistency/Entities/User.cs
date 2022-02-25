using Microsoft.AspNetCore.Identity;
using WorkSchedule.Application.Constants;

namespace WorkSchedule.Application.Persistency.Entities
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<MorningSchedule> MorningSchedules { get; set; }
        public ICollection<Forenoonschedule> ForenoonSchedules { get; set; }
        public ICollection<HolidaySchedule> HolidaySchedules { get; set; }
        public UserRole Role { get; set; }
    }
}
