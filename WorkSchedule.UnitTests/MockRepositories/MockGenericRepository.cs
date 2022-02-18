using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WorkSchedule.Application.Data.Repositories;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.UnitTests.MockRepositories
{
    public class MockGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public List<TEntity> Entities { get; set; }
        public MockGenericRepository(List<TEntity> entities)
        {
            Entities = entities;
        }
        public Mock<IRepository<TEntity>> GetGenericRepository()
        {
            
            var mockRepo = new Mock<IRepository<TEntity>>();
            mockRepo.Setup(r => r.Get(It.IsAny<Expression<Func<TEntity, bool>>>(), 
                    It.IsAny<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(), 
                    It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string include, bool noTracking) => Get(filter, orderBy, include, noTracking));
            mockRepo.Setup(r => r.Add(It.IsAny<TEntity>())).Callback<TEntity>(r => Entities.Append(r));
            mockRepo.Setup(r => r.GetByID(It.IsAny<Guid>())).ReturnsAsync((Guid id) => Entities.FirstOrDefault(r => r.Id == id));
            mockRepo.Setup(r => r.Delete(It.IsAny<TEntity>())).Callback<TEntity>(r => Entities.Remove(r)).Verifiable();
            mockRepo.Setup(r => r.FindAsync(It.IsAny<Expression<Func<TEntity, bool>>>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((Expression<Func<TEntity, bool>> exp, string include, bool noTracking) => FindAsync(exp));
            return mockRepo;
        }

        private List<TEntity> FindAsync(Expression<Func<TEntity, bool>> exp)
        {
            IQueryable<TEntity> query = Entities.AsQueryable();
            query = query.Where(exp);
            return query.ToList();
        }

        private List<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, 
            string include = "", bool noTracking = false)
        {
            IQueryable<TEntity> query = Entities.AsQueryable();

            if (noTracking)
            {
                query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }


            foreach (var includeProperty in include.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }


            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
