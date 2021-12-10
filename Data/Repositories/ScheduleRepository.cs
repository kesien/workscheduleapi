using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Data.Repositories
{
    public class ScheduleRepository : Repository<MonthlySchedule>
    {
        private readonly DbSet<MonthlySchedule> _dbSet;
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _dbSet = context.Set<MonthlySchedule>();
        }

        public Task<MonthlySchedule> GetByDate(int year, int month, Func<IQueryable<MonthlySchedule>, IOrderedQueryable<MonthlySchedule>> orderBy = null)
        {
            IQueryable<MonthlySchedule> query = _dbSet;
            return query.Where(schedule => schedule.Year == year && schedule.Month == month)
                        .Include(schedule => schedule.Days)
                        .ThenInclude(day => day.UsersScheduledForMorning)
                        .ThenInclude(morning => morning.User)
                        .Include(schedule => schedule.Days)
                        .ThenInclude(day => day.UsersScheduledForForenoon)
                        .ThenInclude(forenoon => forenoon.User)
                        .Include(schedule => schedule.Days)
                        .ThenInclude(day => day.UsersOnHoliday)
                        .ThenInclude(holiday => holiday.User)
                        .Include(schedule => schedule.Summaries)
                        .Include(schedule => schedule.WordFile)
                        .FirstOrDefaultAsync();
        }
    }
}