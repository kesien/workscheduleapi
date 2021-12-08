using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkScheduleMaker.Entities
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