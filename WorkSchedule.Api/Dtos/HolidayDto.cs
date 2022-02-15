using System.ComponentModel.DataAnnotations;

namespace WorkSchedule.Api.Dtos
{
    public class HolidayDto
    {
        public Guid? Id { get; set; }
        [Range(2000, 3000)]
        public int Year { get; set; }
        [Range(1, 12)]
        public int Month { get; set; }
        [Range(1, 31)]
        public int Day { get; set; }
        public bool IsFix { get; set; }
    }
}