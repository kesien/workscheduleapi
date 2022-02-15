using System.Linq.Expressions;
namespace WorkSchedule.Application.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);
        IEnumerable<TEntity> GetAsNoTracking();
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool noTracking = false);
        TEntity? GetByID(object id);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        Task<TEntity>? FindAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "", bool noTracking = false);
        void Update(TEntity entityToUpdate);
    }
}
