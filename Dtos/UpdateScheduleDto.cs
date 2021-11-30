using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkScheduleMaker.Dtos
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