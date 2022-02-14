using System.ComponentModel.DataAnnotations;
using WorkSchedule.Api.Constants;

namespace WorkSchedule.Api.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(40, MinimumLength = 4)]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
