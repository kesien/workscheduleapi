using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkScheduleMaker.Entities
{
    public class WordFile : BaseEntity
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public Guid MonthlyScheduleId { get; set; }
        public MonthlySchedule MonthlySchedule { get; set; }
    }
}