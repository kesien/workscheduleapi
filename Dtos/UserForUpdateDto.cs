using System.ComponentModel.DataAnnotations;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Dtos
{
    public class UserForUpdateDto
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
