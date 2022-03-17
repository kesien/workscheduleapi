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
    public class MockUsersRepository : MockGenericRepository<User>
    {
        public List<Request> Requests { get; set; }
        public MockUsersRepository(List<User> entities, List<Request> requests) : base(entities)
        {
            Requests = requests;
        }

        public Mock<UsersRepository> GetScheduleRepository()
        {
            var mockRepo = new Mock<UsersRepository>();
            mockRepo.Setup(r => r.Get(It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                    It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
            string include, bool noTracking) => Get(filter, orderBy, include, noTracking));
            mockRepo.Setup(r => r.Add(It.IsAny<User>())).Callback<User>(r => Entities.Append(r));
            mockRepo.Setup(r => r.GetByID(It.IsAny<Guid>())).ReturnsAsync((Guid id) => Entities.FirstOrDefault(r => r.Id == id));
            mockRepo.Setup(r => r.Delete(It.IsAny<User>())).Callback<User>(r => Entities.Remove(r)).Verifiable();
            mockRepo.Setup(r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((Expression<Func<User, bool>> exp, string include, bool noTracking) => FindAsync(exp));
            mockRepo.Setup(r => r.GetWithRequestByDate(It.IsAny<int>(), It.IsAny<int>())).Returns(Entities);
            return mockRepo;
        }

        private List<User> FindAsync(Expression<Func<User, bool>> exp)
        {
            IQueryable<User> query = Entities.AsQueryable();
            query = query.Where(exp);
            return query.ToList();
        }

        private List<User> Get(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
            string include = "", bool noTracking = false)
        {
            IQueryable<User> query = Entities.AsQueryable();

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
