using AutoMapper;
using FluentAssertions;
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
    public class DeleteRequestCommandHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IRequestService _requestService;
        public DeleteRequestCommandHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
            _requestService = new RequestService(_uow);
        }

        [Fact]
        public async Task ValidId_Should_BeDeleted()
        {
            var command = new DeleteRequestCommand { Id = Guid.Parse("ce17f790-3a10-4f0e-0000-558f1da49d51") };
            var commandHandler = new DeleteRequestCommandHandler(_requestService);

            await commandHandler.Handle(command, CancellationToken.None);
            var requests = await _uow.RequestRepository.Get();

            requests.Count().Should().Be(1);
            requests.Where(r => r.Id == Guid.Parse("ce17f790-3a10-4f0e-0000-558f1da49d51")).FirstOrDefault().Should().BeNull();
        }

        [Fact]
        public async Task IdNotFound_ShouldNot_BeDeleted()
        {
            var command = new DeleteRequestCommand { Id = Guid.Parse("ce17f790-3a10-4f0e-1111-558f1da49d51") };
            var commandHandler = new DeleteRequestCommandHandler(_requestService);

            await commandHandler.Handle(command, CancellationToken.None);
            var requests = await _uow.RequestRepository.Get();

            requests.Count().Should().Be(2);
            var request = requests.Where(r => r.Id == Guid.Parse("ce17f790-3a10-4f0e-0000-558f1da49d51")).FirstOrDefault();
            request.Should().NotBeNull();
        }

        [Fact]
        public async Task InValidId_Should_ShouldTrowError()
        {
            var command = new DeleteRequestCommand { Id = Guid.Empty };
            var commandHandler = new DeleteRequestCommandHandler(_requestService);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var requests = await _uow.RequestRepository.Get();

            requests.Count().Should().Be(2);
            var request = requests.Where(r => r.Id == Guid.Parse("ce17f790-3a10-4f0e-0000-558f1da49d51")).FirstOrDefault();
            request.Should().NotBeNull();
        }
    }
}
