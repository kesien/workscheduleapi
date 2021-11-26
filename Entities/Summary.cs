using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkScheduleMaker.Entities
{
    public class Summary
    {
        public Guid Id { get; set; }
        public MonthlySchedule MonthlySchedule { get; set; }
        public string MonhtlyScheduleId { get; set; }
        public string Name { get; set; }
        public int Morning { get; set; }
        public int Forenoon { get; set; }
        public int Holiday { get; set; }
    }
}