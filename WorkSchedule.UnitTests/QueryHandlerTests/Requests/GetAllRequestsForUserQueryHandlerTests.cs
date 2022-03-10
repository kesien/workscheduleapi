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
    public class GetAllRequestsForUserQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IRequestService _requestService;

        public GetAllRequestsForUserQueryHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
            _requestService = new RequestService(_uow);
        }

        [Fact]
        public async Task GetAllRequestsQueryHandler_Should_ReturnAllRequests()
        {
            var query = new GetAllRequestsForUserQuery() { UserId = Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d52") };
            var handler = new GetAllRequestsForUserQueryHandler(_requestService, _mapper);
            var requests = await handler.Handle(query, CancellationToken.None);
            requests.Should().NotBeNull();
            requests.Count.Should().Be(1);
            requests[0].Should().NotBeNull();
            requests[0].UserId.ToString().Should().Be("ce17f790-3a10-4f0e-b2cf-558f1da49d52");
            requests[0].Username.Should().Be("test@test.com");
            requests[0].Id.GetType().Should().Be(typeof(Guid));
            requests[0].Date.Should().BeSameDateAs(DateTime.Parse("2022-03-24"));
            requests[0].Type.Should().Be(Api.Constants.RequestType.FORENOON);
        }

        [Fact]
        public async Task InvalidUser_Should_ReturnAnEmptyList()
        {
            var query = new GetAllRequestsForUserQuery() { UserId = Guid.Parse("b0844c05-e80b-442e-1111-25470ee6c970") };
            var handler = new GetAllRequestsForUserQueryHandler(_requestService, _mapper);
            var requests = await handler.Handle(query, CancellationToken.None);
            requests.Should().NotBeNull();
            requests.Should().BeEmpty();
        }
    }
}
