using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Users;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.QueryHandlers.Users;
using WorkSchedule.UnitTests.Data;
using WorkSchedule.UnitTests.Helpers;
using Xunit;

namespace WorkSchedule.UnitTests.QueryHandlerTests.Users
{
    public class GetUserByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public GetUserByIdQueryHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            var userStore = new UserStore<User, Role, ApplicationDbContext, Guid>(_context);
            _userManager = new UserManager<User>(userStore, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task ValidIdAndValidRequesterId_Should_ReturnAUser()
        {
            var query = new GetUserByIdQuery { Id = "ce17f790-3a10-4f0e-b2cf-558f1da49d52", RequesterId = "ce17f790-3a10-4f0e-b2cf-558f1da49d52" };
            var queryHandler = new GetUserByIdQueryHandler(_userManager, _mapper);
            var user = await queryHandler.Handle(query, CancellationToken.None);

            user.Should().NotBeNull();
            user.Id.Should().Be("ce17f790-3a10-4f0e-b2cf-558f1da49d52");
            user.Id.GetType().Should().Be(typeof(Guid));
            user.Role.Should().Be(Api.Constants.UserRole.ADMIN);
            user.UserName.Should().Be("test@test.com");
            user.Name.Should().Be("test");
        }

        [Fact]
        public async Task InValidIdButValidRequesterId_Should_ThrowError()
        {
            var query = new GetUserByIdQuery { Id = Guid.Empty.ToString(), RequesterId = "ce17f790-3a10-4f0e-b2cf-558f1da49d52" };
            var queryHandler = new GetUserByIdQueryHandler(_userManager, _mapper);
            queryHandler.Awaiting(y => y.Handle(query, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Contains("You don't have permission to access this data!"));
        }

        [Fact]
        public async Task ValidIdButInValidRequesterId_Should_ThrowError()
        {
            var query = new GetUserByIdQuery { Id = "ce17f790-3a10-4f0e-b2cf-558f1da49d52", RequesterId = Guid.Empty.ToString() };
            var queryHandler = new GetUserByIdQueryHandler(_userManager, _mapper);
            queryHandler.Awaiting(y => y.Handle(query, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Contains("You don't have permission to access this data!"));
        }
    }
}
