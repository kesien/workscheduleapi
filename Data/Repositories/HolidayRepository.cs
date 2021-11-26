using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Data.Repositories
{
    public class HolidayRepository : Repository<ApplicationDbContext, Holiday>
    {
        public HolidayRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}