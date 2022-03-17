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
    public class MockScheduleRepository : MockGenericRepository<MonthlySchedule>
    {
        public MockScheduleRepository(List<MonthlySchedule> entities) : base(entities)
        {
        }

        public Mock<ScheduleRepository> GetScheduleRepository()
        {
            var mockRepo = new Mock<ScheduleRepository>();
            mockRepo.Setup(r => r.Get(It.IsAny<Expression<Func<MonthlySchedule, bool>>>(),
                    It.IsAny<Func<IQueryable<MonthlySchedule>, IOrderedQueryable<MonthlySchedule>>>(),
                    It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((Expression<Func<MonthlySchedule, bool>> filter, Func<IQueryable<MonthlySchedule>, IOrderedQueryable<MonthlySchedule>> orderBy,
            string include, bool noTracking) => Get(filter, orderBy, include, noTracking));
            mockRepo.Setup(r => r.Add(It.IsAny<MonthlySchedule>())).Callback<MonthlySchedule>(r => Entities.Append(r));
            mockRepo.Setup(r => r.GetByID(It.IsAny<Guid>())).ReturnsAsync((Guid id) => Entities.FirstOrDefault(r => r.Id == id));
            mockRepo.Setup(r => r.Delete(It.IsAny<MonthlySchedule>())).Callback<MonthlySchedule>(r => Entities.Remove(r)).Verifiable();
            mockRepo.Setup(r => r.FindAsync(It.IsAny<Expression<Func<MonthlySchedule, bool>>>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((Expression<Func<MonthlySchedule, bool>> exp, string include, bool noTracking) => FindAsync(exp));
            mockRepo.Setup(r => r.GetByDate(It.IsAny<int>(), It.IsAny<int>())).Returns((int year, int month) => GetByDate(year, month));
            return mockRepo;
        }

        private List<MonthlySchedule> FindAsync(Expression<Func<MonthlySchedule, bool>> exp)
        {
            IQueryable<MonthlySchedule> query = Entities.AsQueryable();
            query = query.Where(exp);
            return query.ToList();
        }

        private List<MonthlySchedule> GetByDate(int year, int month)
        {
            return Entities.Where(schedule => schedule.Year == year && schedule.Month == month).ToList();
        }

        private List<MonthlySchedule> Get(Expression<Func<MonthlySchedule, bool>> filter, Func<IQueryable<MonthlySchedule>, IOrderedQueryable<MonthlySchedule>> orderBy,
            string include = "", bool noTracking = false)
        {
            IQueryable<MonthlySchedule> query = Entities.AsQueryable();

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
