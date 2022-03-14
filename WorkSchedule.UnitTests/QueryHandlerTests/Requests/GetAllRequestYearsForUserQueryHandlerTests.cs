using AutoMapper;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Requests;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.QueryHandlers.Requests;
using WorkSchedule.Application.Services.RequestService;
using WorkSchedule.UnitTests.Data;
using WorkSchedule.UnitTests.Helpers;
using Xunit;

namespace WorkSchedule.UnitTests.QueryHandlerTests.Requests
{
    public class GetAllRequestYearsForUserQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IRequestService _requestService;
        public GetAllRequestYearsForUserQueryHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
            _requestService = new RequestService(_uow);
        }

        [Fact]
        public async Task GetAllRequestYearsQueryHandler_Should_ReturnAllYears()
        {
            var query = new GetAllRequestYearsForUserQuery() { UserId = Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d52") };
            var handler = new GetAllRequestYearsForUserQueryHandler(_requestService);
            var years = await handler.Handle(query, CancellationToken.None);
            years.Should().NotBeNull();
            years.Years.Count.Should().Be(1);
            years.Years[0].Should().Be(2022);
        }

        [Fact]
        public async Task InvalidUser_Should_ReturnAnEmptyArray()
        {
            var query = new GetAllRequestYearsForUserQuery() { UserId = Guid.Parse("b0844c05-e80b-442e-1111-25470ee6c970") };
            var handler = new GetAllRequestYearsForUserQueryHandler(_requestService);
            var years = await handler.Handle(query, CancellationToken.None);
            years.Should().NotBeNull();
            years.Years.Should().BeEmpty();
        }
    }
}
