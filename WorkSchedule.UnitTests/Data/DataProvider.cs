using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.UnitTests.Data
{
    public class DataProvider
    {
        private List<User> _users;
        private List<Holiday> _holidays;
        private List<Request> _requests;

        public ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            return new ApplicationDbContext(options);
        }

        public void SeedData(ApplicationDbContext context)
        {
            _users = GenerateUsers();
            _holidays = GenerateHolidays();
            _requests = GenerateRequests();
            context.Users.AddRange(_users);
            context.Holidays.AddRange(_holidays);
            context.Requests.AddRange(_requests);
            context.SaveChanges();
        }
                
        private List<User> GenerateUsers()
        {
            return new List<User>
            {
                new User { Id = Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d52"), Name = "test", UserName = "test@test.com", Role = Application.Constants.UserRole.ADMIN },
                new User { Id = Guid.Parse("035a7a71-fa01-4128-9844-f78813819a75"), Name = "test2", UserName = "test2@test.com" },
                new User { Id = Guid.Parse("89c6b2f2-cf01-480c-9c16-090e8b56f406"), Name = "test3", UserName = "test3@test.com" },
                new User { Id = Guid.Parse("4ca02be1-5e30-41eb-989a-95160c433d43"), Name = "test4", UserName = "test4@test.com" },
            };
        }

        private List<Holiday> GenerateHolidays()
        {
            return new List<Holiday>
            {
                new Holiday { Id = Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d51"), Day = 4, Month = 3, Year = 2022, IsFix = false },
                new Holiday { Id = Guid.NewGuid(), Day = 25, Month = 3, Year = 0, IsFix = true },
                new Holiday { Id = Guid.NewGuid(), Day = 10, Month = 3, Year = 2022, IsFix = false },
            };
        }

        private List<Request> GenerateRequests()
        {
            return new List<Request>
            {
                new Request { Id = Guid.NewGuid(), Date = DateTime.Parse("2022-03-24"), Type = Api.Constants.RequestType.FORENOON, User = _users.First()},
                new Request { Id = Guid.Parse("ce17f790-3a10-4f0e-0000-558f1da49d51"), Date = DateTime.Parse("2022-03-10"), Type = Api.Constants.RequestType.MORNING, User = _users.Last()},
            };
        }
    }
}
