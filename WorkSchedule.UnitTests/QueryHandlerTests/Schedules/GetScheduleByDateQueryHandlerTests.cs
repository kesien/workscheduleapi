using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Schedules;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.QueryHandlers.Schedules;
using WorkSchedule.Application.Services.FileService;
using WorkSchedule.Application.Services.RequestService;
using WorkSchedule.Application.Services.ScheduleService;
using WorkSchedule.UnitTests.Data;
using WorkSchedule.UnitTests.Helpers;
using WorkSchedule.UnitTests.MockRepositories;
using WorkSchedule.UnitTests.MockServices;
using Xunit;

namespace WorkSchedule.UnitTests.QueryHandlerTests.Schedules
{
    public class GetScheduleByDateQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IScheduleService _scheduleService;
        public GetScheduleByDateQueryHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
            var fileService = new Mock<IFileService>().Object;
            _scheduleService = new ScheduleService(_uow, fileService);
        }

        [Fact]
        public async Task ValidDateShould_Return_NewSchedule()
        {
            var query = new GetScheduleByDateQuery { Year = 2022, Month = 03 };
            var queryHandler = new GetScheduleByDateQueryHandler(_scheduleService, _mapper);
            var schedule = await queryHandler.Handle(query, CancellationToken.None);

            schedule.Should().NotBeNull();
            schedule.Days.Count.Should().Be(31);
            schedule.Id.GetType().Should().Be(typeof(Guid));
            schedule.IsSaved.Should().BeFalse();
            schedule.Month.Should().Be(3);
            schedule.Year.Should().Be(2022);
            schedule.NumOfWorkdays.Should().Be(0);
            schedule.WordFile.Should().NotBeNull();
            schedule.Summaries.Should().BeEmpty();

            schedule.Days[9].UsersScheduledForMorning.Count.Should().Be(1);
            schedule.Days[9].UsersScheduledForMorning[0].UserName.Should().Be("test4@test.com");
            schedule.Days[9].UsersScheduledForMorning[0].Name.Should().Be("test4");
            schedule.Days[9].UsersScheduledForMorning[0].IsRequest.Should().BeTrue();
            schedule.Days[9].UsersScheduledForMorning[0].Id.Should().Be("4ca02be1-5e30-41eb-989a-95160c433d43");
            schedule.Days[23].UsersScheduledForForenoon[0].UserName.Should().Be("test@test.com");
            schedule.Days[23].UsersScheduledForForenoon[0].Name.Should().Be("test");
            schedule.Days[23].UsersScheduledForForenoon[0].IsRequest.Should().BeTrue();
            schedule.Days[23].UsersScheduledForForenoon[0].Id.Should().Be("ce17f790-3a10-4f0e-b2cf-558f1da49d52");
            schedule.Days[3].IsHoliday.Should().BeTrue();
            schedule.Days[24].IsHoliday.Should().BeTrue();
            schedule.Days[9].IsHoliday.Should().BeTrue();
            TestScheduleDays(schedule.Days);
        }

        [Fact]
        public async Task InValidDateShould_Return_NewSchedule()
        {

        }

            private void TestScheduleDays(List<Api.Dtos.DayDto> days)
        {
            for (int i = 0; i < days.Count; i++)
            {
                var day = days[i];
                day.Should().NotBeNull();
                day.Date.Should().BeSameDateAs(DateTime.Parse($"2022-03-{i + 1}"));
                day.IsWeekend.Should().Be(IsWeekend(day.Date));
                //if (!day.IsHoliday || !day.IsWeekend) day.UsersScheduledForMorning.Should().NotBeEmpty();
                //if (!day.IsHoliday || !day.IsWeekend) day.UsersScheduledForForenoon.Should().NotBeEmpty();
                day.UsersOnHoliday.Should().BeEmpty();
            }
        }

        private bool IsWeekend(DateTime date) => date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
    }
}
