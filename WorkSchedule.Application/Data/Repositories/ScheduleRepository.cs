using Microsoft.EntityFrameworkCore;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Data.Repositories
{
    public class ScheduleRepository : Repository<MonthlySchedule>
    {
        private readonly DbSet<MonthlySchedule> _dbSet;
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _dbSet = context.Set<MonthlySchedule>();
        }

        public async Task<MonthlySchedule> GetByDate(int year, int month)
        {
            IQueryable<MonthlySchedule> query = _dbSet;
            return await query.Where(schedule => schedule.Year == year && schedule.Month == month)
                        .Include(schedule => schedule.Days.OrderBy(day => day.Date))
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