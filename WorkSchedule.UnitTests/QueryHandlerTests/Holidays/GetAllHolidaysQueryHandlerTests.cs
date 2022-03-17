using AutoMapper;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Holidays;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.QueryHandlers.Holidays;
using WorkSchedule.UnitTests.Data;
using WorkSchedule.UnitTests.Helpers;
using Xunit;

namespace WorkSchedule.UnitTests.QueryHandlerTests.Holidays
{
    public class GetAllHolidaysQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public GetAllHolidaysQueryHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
        }

        [Fact]
        public async Task GetAllHolidaysQueryHandler_Should_ReturnAllHolidays()
        {
            var query = new GetAllHolidaysQuery();
            var queryHandler = new GetAllHolidaysQueryHandler(_uow, _mapper);
            var holidays = await queryHandler.Handle(query, CancellationToken.None);
            holidays.Count.Should().Be(3);
            holidays[0].Should().NotBeNull();
            holidays[0].Id.GetType().Should().Be(typeof(Guid));
            holidays[0].Day.Should().Be(4);
            holidays[0].IsFix.Should().BeFalse();
            holidays[0].Month.Should().Be(3);
            holidays[0].Year.Should().Be(2022);
            holidays[1].Should().NotBeNull();
            holidays[1].Id.GetType().Should().Be(typeof(Guid));
            holidays[1].Day.Should().Be(25);
            holidays[1].IsFix.Should().BeTrue();
            holidays[1].Month.Should().Be(3);
            holidays[1].Year.Should().Be(0);
            holidays[2].Id.GetType().Should().Be(typeof(Guid));
            holidays[2].Day.Should().Be(10);
            holidays[2].IsFix.Should().BeFalse();
            holidays[2].Month.Should().Be(3);
            holidays[2].Year.Should().Be(2022);
        }
    }
}
