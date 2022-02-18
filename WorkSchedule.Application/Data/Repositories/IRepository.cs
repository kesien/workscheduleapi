using System.Linq.Expressions;
namespace WorkSchedule.Application.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entity);
        IEnumerable<TEntity> GetAsNoTracking();
        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "", bool noTracking = false);
        Task<TEntity> GetByID(object id);
        Task Delete(object id);
        void Delete(TEntity entityToDelete);
        Task<List<TEntity>>? FindAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "", bool noTracking = false);
        void Update(TEntity entityToUpdate);
    }
}
