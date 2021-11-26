using Microsoft.AspNetCore.Identity;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Request> Requests { get; set; }
        public UserRole Role { get; set; }
    }
}
