using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Data.Repositories
{
    public class ScheduleRepository : Repository<ApplicationDbContext, MonthlySchedule>
    {
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
