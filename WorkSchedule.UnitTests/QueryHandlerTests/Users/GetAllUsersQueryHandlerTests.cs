using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Users;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.QueryHandlers.Users;
using WorkSchedule.UnitTests.Data;
using WorkSchedule.UnitTests.Helpers;
using Xunit;

namespace WorkSchedule.UnitTests.QueryHandlerTests.Users
{
    public class GetAllUsersQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public GetAllUsersQueryHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
        }

        [Fact]
        public async Task GetAllUsersQueryHandler_Should_ReturnAllUsers()
        {
            var query = new GetAllUsersQuery();
            var queryHandler = new GetAllUsersQueryHandler(_uow, _mapper);
            var users = await queryHandler.Handle(query, CancellationToken.None);

            users.Should().NotBeNull();
            users.Count.Should().Be(4);

            var user = users.First();
            var user2 = users.Last();
            user.Should().NotBeNull();
            user.Id.Should().Be("ce17f790-3a10-4f0e-b2cf-558f1da49d52");
            user.Id.GetType().Should().Be(typeof(Guid));
            user.Role.Should().Be(Api.Constants.UserRole.ADMIN);
            user.UserName.Should().Be("test@test.com");
            user.Name.Should().Be("test");

            user2.Should().NotBeNull();
            user2.Id.Should().Be("4ca02be1-5e30-41eb-989a-95160c433d43");
            user2.Id.GetType().Should().Be(typeof(Guid));
            user2.UserName.Should().Be("test4@test.com");
            user2.Name.Should().Be("test4");
        }
    }
}
