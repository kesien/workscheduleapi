using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Data.Repositories
{
    public class DayRepository : Repository<ApplicationDbContext, Day>
    {
        public DayRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}