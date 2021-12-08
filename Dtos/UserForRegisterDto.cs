using System.ComponentModel.DataAnnotations;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(20, MinimumLength = 4)]
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
