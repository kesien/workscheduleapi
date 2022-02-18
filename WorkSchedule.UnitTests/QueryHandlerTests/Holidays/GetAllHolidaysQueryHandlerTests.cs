using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Holidays;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.QueryHandlers.Holidays;
using WorkSchedule.UnitTests.Helpers;
using WorkSchedule.UnitTests.MockRepositories;
using Xunit;

namespace WorkSchedule.UnitTests.QueryHandlerTests.Holidays
{
    public class GetAllHolidaysQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly MockGenericRepository<Holiday> holidayRepo;
        public GetAllHolidaysQueryHandlerTests()
        {
            List<Holiday> entities = GenerateEntities();
            holidayRepo = new MockGenericRepository<Holiday>(entities);
            var uowMock = MockUnitOfWork.GetUnitOfWorkMock();
            uowMock.Setup(r => r.HolidayRepository).Returns(holidayRepo.GetGenericRepository().Object);
            _uow = uowMock.Object;
            _mapper = MappingHelper.GetMappings().CreateMapper();
        }

        [Fact]
        public async Task GetAllHolidaysQueryHandler_Should_ReturnAllHolidays()
        {
            var query = new GetAllHolidaysQuery();
            var queryHandler = new GetAllHolidaysQueryHandler(_uow, _mapper);
            var holidays = await queryHandler.Handle(query, CancellationToken.None);
            holidays.Count.Should().Be(3);
            holidays[0].Should().NotBeNull();
            holidays[0].Id.Should().Be("7c1f48c0-97fe-4b54-9b4d-73cad8c34634");
            holidays[0].Day.Should().Be(1);
            holidays[0].IsFix.Should().BeTrue();
            holidays[0].Month.Should().Be(1);
            holidays[0].Year.Should().Be(2022);
            holidays[1].Should().NotBeNull();
            holidays[1].Id.Should().Be("7c1f48c0-97fe-4b54-9b4c-73cad8c34634");
            holidays[1].Day.Should().Be(2);
            holidays[1].IsFix.Should().BeTrue();
            holidays[1].Month.Should().Be(1);
            holidays[1].Year.Should().Be(2022);
            holidays[2].Id.Should().Be("7c1f48c0-97fe-4b54-9b4e-73cad8c34634");
            holidays[2].Day.Should().Be(3);
            holidays[2].IsFix.Should().BeFalse();
            holidays[2].Month.Should().Be(1);
            holidays[2].Year.Should().Be(2022);
        }

        [Fact]
        public async Task GetAllHolidaysQueryHandler_Should_EmptyListIfThereAreNoHolidays()
        {
            holidayRepo.Entities.Clear();
            var query = new GetAllHolidaysQuery();
            var queryHandler = new GetAllHolidaysQueryHandler(_uow, _mapper);
            var holidays = await queryHandler.Handle(query, CancellationToken.None);
            holidays.Should().NotBeNull();
            holidays.Should().BeEmpty();
        }

        private List<Holiday> GenerateEntities()
        {
            var entities = new List<Holiday>
            {
                new Holiday
                {
                    Id = Guid.Parse("7c1f48c0-97fe-4b54-9b4d-73cad8c34634"),
                    Day = 1,
                    IsFix = true,
                    Month = 1,
                    Year = 2022
                },
                new Holiday
                {
                    Id = Guid.Parse("7c1f48c0-97fe-4b54-9b4c-73cad8c34634"),
                    Day = 2,
                    IsFix = true,
                    Month = 1,
                    Year = 2022
                },
                new Holiday
                {
                    Id = Guid.Parse("7c1f48c0-97fe-4b54-9b4e-73cad8c34634"),
                    Day = 3,
                    IsFix = false,
                    Month = 1,
                    Year = 2022
                }
            };
            return entities;
        }
    }
}
