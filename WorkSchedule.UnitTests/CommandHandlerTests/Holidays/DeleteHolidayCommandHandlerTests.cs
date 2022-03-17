using FluentAssertions;
using Moq;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Application.CommandHandlers.Holidays;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Services.HolidayService;
using WorkSchedule.UnitTests.Data;
using Xunit;

namespace WorkSchedule.UnitTests.CommandHandlerTests.Holidays
{
    public class DeleteHolidayCommandHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _context;
        private readonly IHolidayService _holidayService;
        private readonly ILogger _logger;
        public DeleteHolidayCommandHandlerTests()
        {
            var dp = new DataProvider();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
            _holidayService = new HolidayService(_uow);
            _logger = new Mock<ILogger>().Object;
        }

        [Fact]
        public async Task HolidayWithValidId_Should_BeDeleted()
        {
            var command = new DeleteHolidayCommand { Id = Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d51") };
            var commandHandler = new DeleteHolidayCommandHandler(_uow, _logger);

            await commandHandler.Handle(command, CancellationToken.None);
            var holidays = await _uow.HolidayRepository.Get();
            var holiday = holidays.Where(h => h.Id == Guid.Parse("ce17f790-3a10-4f0e-b2cf-558f1da49d51")).FirstOrDefault();

            holidays.Count().Should().Be(2);
            holiday.Should().BeNull();
        }

        [Fact]
        public async Task HolidayWithInValidId_ShouldNot_BeDeleted()
        {
            var command = new DeleteHolidayCommand { Id = Guid.Parse("ce17f790-3a10-4f0e-b2cf-000000000000") };
            var commandHandler = new DeleteHolidayCommandHandler(_uow, _logger);

            await commandHandler.Handle(command, CancellationToken.None);
            var holidays = await _uow.HolidayRepository.Get();

            holidays.Count().Should().Be(3);
        }

        [Fact]
        public async Task HolidayWithoutId_Should_ThrowError()
        {
            var command = new DeleteHolidayCommand();
            var commandHandler = new DeleteHolidayCommandHandler(_uow, _logger);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>();

            var holidays = await _uow.HolidayRepository.Get();

            holidays.Count().Should().Be(3);
        }
    }
}
