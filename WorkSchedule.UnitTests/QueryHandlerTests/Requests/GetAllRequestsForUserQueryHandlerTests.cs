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
    public class GetAllRequestsForUserQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;
        private readonly MockGenericRepository<Request> fileRepoMock;

        public GetAllRequestsForUserQueryHandlerTests()
        {
            var entities = GenerateEntities();
            var uowMock = MockUnitOfWork.GetUnitOfWorkMock();
            fileRepoMock = new MockGenericRepository<Request>(entities);
            uowMock.Setup(r => r.RequestRepository).Returns(fileRepoMock.GetGenericRepository().Object);
            _uow = uowMock.Object;
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _requestService = new RequestService(_uow);
        }

        [Fact]
        public async Task GetAllRequestsQueryHandler_Should_ReturnAllRequests()
        {
            var query = new GetAllRequestsForUserQuery() { UserId = Guid.Parse("b0844c05-e80b-442e-0000-25470ee6c970") };
            var handler = new GetAllRequestsForUserQueryHandler(_requestService, _mapper);
            var requests = await handler.Handle(query, CancellationToken.None);
            requests.Should().NotBeNull();
            requests.Count.Should().Be(2);
            requests[0].Should().NotBeNull();
            requests[0].UserId.ToString().Should().Be("b0844c05-e80b-442e-0000-25470ee6c970");
            requests[0].Username.Should().Be("test");
            requests[0].Id.Should().Be("7c1f48c0-97fe-4b54-9b4d-73cad8c34634");
            requests[0].Date.Should().BeSameDateAs(DateTime.Parse("2022-01-02"));
            requests[0].Type.Should().Be(Api.Constants.RequestType.FORENOON);

            requests[1].Should().NotBeNull();
            requests[1].UserId.ToString().Should().Be("b0844c05-e80b-442e-0000-25470ee6c970");
            requests[1].Username.Should().Be("test");
            requests[1].Id.Should().Be("7c1f48c0-97fe-4b54-9b4e-73cad8c34634");
            requests[1].Date.Should().BeSameDateAs(DateTime.Parse("2022-01-03"));
            requests[1].Type.Should().Be(Api.Constants.RequestType.MORNING);
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

        private List<Request> GenerateEntities()
        {
            var user = new User { Id = Guid.Parse("b0844c05-e80b-442e-0000-25470ee6c970"), Name = "test", UserName = "test" };
            var user2 = new User { Id = Guid.Parse("b0844c05-e80b-442e-0001-25470ee6c970"), Name = "test2", UserName = "test2" };
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
                    User = user2,
                    Date = DateTime.Parse("2022-02-04"),
                    Type = Api.Constants.RequestType.HOLIDAY
                }
            };
            return entities;
        }
    }
}
