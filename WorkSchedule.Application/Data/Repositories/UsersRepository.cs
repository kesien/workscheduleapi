using Microsoft.EntityFrameworkCore;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Data.Repositories
{
    public class UsersRepository : Repository<User>
    {
        private readonly DbSet<User> _dbSet;
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
            _dbSet = context.Set<User>();
        }

        public IEnumerable<User> GetWithRequestByDate(int year, int month)
        {
            return _dbSet.AsNoTracking().Include(user => user.Requests.Where(request => request.Date.Year == year && request.Date.Month == month)).AsNoTracking().ToList();
        }
    }
}