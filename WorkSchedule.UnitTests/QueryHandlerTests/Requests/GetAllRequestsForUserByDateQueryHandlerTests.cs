using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Requests;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.QueryHandlers.Requests;
using WorkSchedule.Application.Services.RequestService;
using WorkSchedule.UnitTests.Helpers;
using WorkSchedule.UnitTests.MockRepositories;
using Xunit;

namespace WorkSchedule.UnitTests.QueryHandlerTests.Requests
{
    public class GetAllRequestsForUserByDateQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IRequestService _requestService;
        public GetAllRequestsForUserByDateQueryHandlerTests()
        {
            _mapper = MappingHelper.GetMappings().CreateMapper();
            var uowMock = MockUnitOfWork.GetUnitOfWorkMock();
            var entitites = GenerateEntities();
            var repo = new MockGenericRepository<Request>(entitites);
            uowMock.Setup(r => r.RequestRepository).Returns(repo.GetGenericRepository().Object);
            _uow = uowMock.Object;
            _requestService = new RequestService(_uow);
        }

        [Fact]
        public async Task ValidUserIdAndDate_Should_ReturnAllRequestsForGivenUser()
        {
            var query = new GetAllRequestsForUserByDateQuery() { Month = 1, Year = 2022, UserId = Guid.Parse("b0844c05-e80b-442e-b488-25470ee6c970") };
            var handler = new GetAllRequestsForUserByDateQueryHandler(_mapper, _requestService);
            var requests = await handler.Handle(query, CancellationToken.None);
            requests.Should().NotBeNull();
            requests.Count.Should().Be(3);
            requests[0].Should().NotBeNull();
            requests[0].UserId.ToString().Should().Be("b0844c05-e80b-442e-b488-25470ee6c970");
            requests[0].Id.Should().Be("7c1f48c0-97fe-4b54-9b4d-73cad8c34634");
            requests[0].Date.Should().BeSameDateAs(DateTime.Parse("2022-01-02"));
            requests[0].Type.Should().Be(Api.Constants.RequestType.FORENOON);

            requests[1].Should().NotBeNull();
            requests[1].UserId.ToString().Should().Be("b0844c05-e80b-442e-b488-25470ee6c970");
            requests[1].Id.Should().Be("7c1f48c0-97fe-4b54-9b4e-73cad8c34634");
            requests[1].Date.Should().BeSameDateAs(DateTime.Parse("2022-01-03"));
            requests[1].Type.Should().Be(Api.Constants.RequestType.MORNING);

            requests[2].Should().NotBeNull();
            requests[2].UserId.ToString().Should().Be("b0844c05-e80b-442e-b488-25470ee6c970");
            requests[2].Id.Should().Be("7c1f48c0-97fe-4b54-9b4f-73cad8c34634");
            requests[2].Date.Should().BeSameDateAs(DateTime.Parse("2022-01-04"));
            requests[2].Type.Should().Be(Api.Constants.RequestType.HOLIDAY);
        }

        [Fact]
        public async Task InValidUserIdButValidDate_Should_ReturnEmptyList()
        {
            var query = new GetAllRequestsForUserByDateQuery() { Month = 1, Year = 2022, UserId = Guid.Parse("b0844c05-e80b-442e-0000-25470ee6c970") };
            var handler = new GetAllRequestsForUserByDateQueryHandler(_mapper, _requestService);
            var requests = await handler.Handle(query, CancellationToken.None);
            requests.Should().NotBeNull();
            requests.Should().BeEmpty();
        }

        [Fact]
        public async Task ValidUserButInvalidDate_Should_ReturnEmptyList()
        {
            var query = new GetAllRequestsForUserByDateQuery() { Month = 2, Year = 2022, UserId = Guid.Parse("b0844c05-e80b-442e-b488-25470ee6c970") };
            var handler = new GetAllRequestsForUserByDateQueryHandler(_mapper, _requestService);
            var requests = await handler.Handle(query, CancellationToken.None);
            requests.Should().NotBeNull();
            requests.Should().BeEmpty();
        }

        private List<Request> GenerateEntities()
        {
            var user = new User { Id = Guid.Parse("b0844c05-e80b-442e-b488-25470ee6c970"), Name = "test", UserName = "test" };
            var entities = new List<Request>
            {
                new Request
                {
                    Id = Guid.Parse("7c1f48c0-97fe-4b54-9b4d-73cad8c34634"),
                    User = user,
                    Date = DateTime.Parse("2022-01-02"),
                    Type = Api.Constants.RequestType.FORENOON
                },
                new Request
                {
                    Id = Guid.Parse("7c1f48c0-97fe-4b54-9b4e-73cad8c34634"),
                    User = user,
                    Date = DateTime.Parse("2022-01-03"),
                    Type = Api.Constants.RequestType.MORNING
                },
                new Request
                {
                    Id = Guid.Parse("7c1f48c0-97fe-4b54-9b4f-73cad8c34634"),
                    User = user,
                    Date = DateTime.Parse("2022-01-04"),
                    Type = Api.Constants.RequestType.HOLIDAY
                }
            };
            return entities;
        }
    }
}
