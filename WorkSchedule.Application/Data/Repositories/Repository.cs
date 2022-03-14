using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorkSchedule.Application.Persistency;

namespace WorkSchedule.Application.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async virtual Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual IEnumerable<TEntity> GetAsNoTracking()
        {
            return _dbSet.AsNoTracking<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "", bool noTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (noTracking)
            {
                query.AsNoTracking<TEntity>();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }


            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }


            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetByID(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Delete(object id)
        {
            TEntity? entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual async Task<List<TEntity>>? FindAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "", bool noTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            query = query.Where(filter);
            if (noTracking)
            {
                query.AsNoTracking();
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
