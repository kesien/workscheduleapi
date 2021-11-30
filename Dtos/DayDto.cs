﻿using System.ComponentModel.DataAnnotations;

namespace WorkScheduleMaker.Dtos
{
    public class DayDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsWeekend { get; set; }
        public List<UserToRequestDto> UsersScheduledForMorning { get; set; }
        public List<UserToRequestDto> UsersScheduledForForenoon { get; set; }
        public List<UserToRequestDto> UsersOnHoliday { get; set; }
    }
}
