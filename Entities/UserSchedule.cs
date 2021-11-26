﻿using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Entities
{
    public class UserSchedule
    {
        public User User { get; set; }
        public int NumOfMorningSchedules { get; set; }
        public int NumOfForenoonSchedules { get; set; }
        public int NumOfHolidays { get; set; }
    }
}