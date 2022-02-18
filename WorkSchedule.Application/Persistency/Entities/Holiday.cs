using System.ComponentModel.DataAnnotations;

namespace WorkSchedule.Application.Persistency.Entities
{
    public class Holiday : BaseEntity
    {
        public int? Year { get; set; }
        [Range(1, 12)]
        public int Month { get; set; }
        [Range(1, 31)]
        public int Day { get; set; }
        public bool IsFix { get; set; }
    }
}