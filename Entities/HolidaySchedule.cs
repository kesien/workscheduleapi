﻿namespace WorkScheduleMaker.Entities
{
    public class HolidaySchedule
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
