using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Data.Repositories
{
    public class RequestRepository : Repository<Request>
    {
        private readonly DbSet<Request> _dbSet;
        public RequestRepository(ApplicationDbContext context) : base(context)
        {
            _dbSet = context.Set<Request>();
        }

        public IEnumerable<Request> GetByDate(int year, int month)
        {
            IQueryable<Request> query = _dbSet;
                return query.Include(request => request.User)
                        .AsNoTracking()
                        .Where(request => request.Date.Year == year && request.Date.Month == month)
                        .AsNoTracking()
                        .ToList();
        }
    }
}