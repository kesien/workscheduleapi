using AutoMapper;
using FluentAssertions;
using Moq;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Commands.Requests;
using WorkSchedule.Application.CommandHandlers.Requests;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Services.RequestService;
using WorkSchedule.UnitTests.Data;
using WorkSchedule.UnitTests.Helpers;
using Xunit;

namespace WorkSchedule.UnitTests.CommandHandlerTests.Requests
{
    public class AddNewRequestCommandHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IRequestService _requestService;
        private readonly ILogger _logger;
        public AddNewRequestCommandHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
            _requestService = new RequestService(_uow);
            _logger = new Mock<ILogger>().Object;
        }

        [Fact]
        public async Task ValidRequest_Should_BeAdded()
        {
            var command = new AddNewRequestCommand
            {
                Date = DateTime.Parse("2030-04-04"),
                Type = Api.Constants.RequestType.MORNING,
                UserId = Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d52")
            };
            var commandHandler = new AddNewRequestCommandHandler(_requestService, _logger);

            await commandHandler.Handle(command, CancellationToken.None);
            var requests = await _uow.RequestRepository.Get(null, null, "User");
            var request = requests.Last();

            requests.Count().Should().Be(3);
            request.Date.Should().Be(DateTime.Parse("2030-04-04"));
            request.Id.GetType().Should().Be(typeof(Guid));
            request.Type.Should().Be(Api.Constants.RequestType.MORNING);
            request.User.Id.Should().Be("ce17f790-3a10-4f0e-b2cf-558f1da49d52");
        }

        [Fact]
        public async Task DateInThePast_Should_ThrowError()
        {
            var command = new AddNewRequestCommand
            {
                Date = DateTime.Parse("2022-02-04"),
                Type = Api.Constants.RequestType.MORNING,
                UserId = Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d52")
            };
            var commandHandler = new AddNewRequestCommandHandler(_requestService, _logger);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var requests = await _uow.RequestRepository.Get();

            requests.Count().Should().Be(2);
        }

        [Fact]
        public async Task WeekendDate_Should_ThrowError()
        {
            var command = new AddNewRequestCommand
            {
                Date = DateTime.Parse("2022-04-03"),
                Type = Api.Constants.RequestType.MORNING,
                UserId = Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d52")
            };
            var commandHandler = new AddNewRequestCommandHandler(_requestService, _logger);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var requests = await _uow.RequestRepository.Get();

            requests.Count().Should().Be(2);
        }

        [Fact]
        public async Task InvalidUser_Should_ThrowError()
        {
            var command = new AddNewRequestCommand
            {
                Date = DateTime.Parse("2022-04-04"),
                Type = Api.Constants.RequestType.MORNING,
                UserId = Guid.Empty
            };
            var commandHandler = new AddNewRequestCommandHandler(_requestService, _logger);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var requests = await _uow.RequestRepository.Get();

            requests.Count().Should().Be(2);
        }

        [Fact]
        public async Task UserAlreadyHasARequestWithTheSameDate_Should_ThrowError()
        {
            var command = new AddNewRequestCommand
            {
                Date = DateTime.Parse("2022-03-24"),
                Type = Api.Constants.RequestType.MORNING,
                UserId = Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d52")
            };
            var commandHandler = new AddNewRequestCommandHandler(_requestService, _logger);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var requests = await _uow.RequestRepository.Get();

            requests.Count().Should().Be(2);
        }
    }
}
