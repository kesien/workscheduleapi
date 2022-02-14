using System.ComponentModel.DataAnnotations;

namespace WorkSchedule.Api.Dtos
{
    public class UpdateScheduleDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public List<DayDto> Days { get; set; }
    }
}