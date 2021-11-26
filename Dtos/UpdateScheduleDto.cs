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
        public DayDto Day { get; set; }
    }
}