using WorkSchedule.Api.Constants;

namespace WorkSchedule.Api.Dtos
{
    public class UserToListDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public bool ReceiveEmails { get; set; }
    }
}
