using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkScheduleMaker.Dtos
{
    public class SummaryDto
    {
        public string Name { get; set; }
        public int Morning { get; set; }
        public int Forenoon { get; set; }
        public int Holiday { get; set; }
    }
}