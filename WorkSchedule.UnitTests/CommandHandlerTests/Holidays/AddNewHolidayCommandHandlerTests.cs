using AutoMapper;
using FluentAssertions;
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
using WorkSchedule.UnitTests.Helpers;
using Xunit;

namespace WorkSchedule.UnitTests.CommandHandlerTests.Holidays
{
    public class AddNewHolidayCommandHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IHolidayService _holidayService;
        public AddNewHolidayCommandHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
            _holidayService = new HolidayService(_uow);
        }

        [Fact]
        public async Task ValidHoliday_Should_BeAdded()
        {
            var command = new AddNewHolidayCommand { Day = 1, Month = 1, IsFix = true, Year = 2022 };
            var commandHandler = new AddNewHolidayCommandHandler(_holidayService);

            await commandHandler.Handle(command, CancellationToken.None);
            var holidays = await _uow.HolidayRepository.Get();
            var holiday = holidays.Last();

            holidays.Count().Should().Be(4);
            holiday.Day.Should().Be(1);
            holiday.Month.Should().Be(1);
            holiday.Year.Should().Be(0);
            holiday.IsFix.Should().BeTrue();
            holiday.Id.GetType().Should().Be(typeof(Guid));
        }

        [Fact]
        public async Task InValidMonth_Should_ThrowError()
        {
            var command = new AddNewHolidayCommand { Day = 1, Month = 100, IsFix = false, Year = 2022 };
            var commandHandler = new AddNewHolidayCommandHandler(_holidayService);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var holidays = await _uow.HolidayRepository.Get();

            holidays.Count().Should().Be(3);
        }

        [Fact]
        public async Task InValidDay_Should_ThrowError()
        {
            var command = new AddNewHolidayCommand { Day = 32, Month = 1, IsFix = false, Year = 2022 };
            var commandHandler = new AddNewHolidayCommandHandler(_holidayService);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var holidays = await _uow.HolidayRepository.Get();

            holidays.Count().Should().Be(3);
        }

        [Fact]
        public async Task PropertyDayMissing_Should_ThrowError()
        {
            var command = new AddNewHolidayCommand { Month = 1, IsFix = false, Year = 2022 };
            var commandHandler = new AddNewHolidayCommandHandler(_holidayService);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var holidays = await _uow.HolidayRepository.Get();

            holidays.Count().Should().Be(3);
        }

        [Fact]
        public async Task PropertyMonthMissing_Should_ThrowError()
        {
            var command = new AddNewHolidayCommand { Day = 1, IsFix = false, Year = 2022 };
            var commandHandler = new AddNewHolidayCommandHandler(_holidayService);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var holidays = await _uow.HolidayRepository.Get();

            holidays.Count().Should().Be(3);
        }

        [Fact]
        public async Task PropertyYearMissing_Should_ThrowError()
        {
            var command = new AddNewHolidayCommand { Day = 1, Month = 1, IsFix = false };
            var commandHandler = new AddNewHolidayCommandHandler(_holidayService);

            commandHandler.Awaiting(y => y.Handle(command, CancellationToken.None)).Should()
                .ThrowAsync<BusinessException>()
                .Where(e => e.ErrorMessages.Count == 1);
            var holidays = await _uow.HolidayRepository.Get();

            holidays.Count().Should().Be(3);
        }
    }
}
