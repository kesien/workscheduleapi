using WorkSchedule.Api.Constants;

namespace WorkSchedule.Api.Dtos
{
    public class UserForRegisterDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
