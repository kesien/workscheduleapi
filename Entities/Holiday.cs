using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkScheduleMaker.Entities
{
    public class Holiday
    {
        public Guid Id { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}