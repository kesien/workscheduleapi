using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Data.Repositories
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