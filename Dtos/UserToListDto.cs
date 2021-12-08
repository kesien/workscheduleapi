using WorkScheduleMaker.Entities;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Dtos
{
    public class UserToListDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}
