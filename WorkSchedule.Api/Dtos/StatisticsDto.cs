using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSchedule.Api.Dtos
{
    public class StatisticsDto
    {
        public List<KeyValuePair<string, int>> Requests { get; set; }
    }
}
