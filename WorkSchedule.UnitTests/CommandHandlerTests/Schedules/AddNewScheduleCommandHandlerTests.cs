using AutoMapper;
using FluentAssertions;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.CommandHandlers.Schedules;
using WorkSchedule.Application.Constants;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Events;
using WorkSchedule.Application.Helpers;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.Services.ScheduleService;
using WorkSchedule.UnitTests.Data;
using WorkSchedule.UnitTests.Helpers;
using WorkSchedule.UnitTests.MockServices;
using Xunit;

namespace WorkSchedule.UnitTests.CommandHandlerTests.Schedules
{
    public class AddNewScheduleCommandHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IScheduleService _scheduleService;
        private readonly ILogger _logger;
        public AddNewScheduleCommandHandlerTests()
        {
            var dp = new DataProvider();
            _mapper = MappingHelper.GetMappings().CreateMapper();
            _context = dp.GetContext();
            dp.SeedData(_context);
            _uow = new UnitOfWork(_context);
            var fileService = MockFileService.GetFileServiceMock(new WordFile
            {
                FileName = "test.docx",
                FilePath = "./",
            });
            _scheduleService = new ScheduleService(_uow, fileService.Object);
            _logger = new Mock<ILogger>().Object;
        }

        [Fact]
        public async Task ValidSchedule_Should_BeAdded()
        {
            var customPublisherMock = new Mock<ICustomPublisher>();
            customPublisherMock.Setup(x => x.Publish(It.IsAny<NewScheduleCreatedEvent>(), It.IsAny<PublishStrategy>(), It.IsAny<CancellationToken>())).Verifiable();
            var command = new AddNewScheduleCommand { Month = 4, Year = 2022, UserId = "ce17f790-3a10-4f0e-b2cf-558f1da49d52" };
            var commandHandler = new AddNewScheduleCommandHandler(_scheduleService, customPublisherMock.Object, _logger);

            await commandHandler.Handle(command, CancellationToken.None);
            var schedule = await _uow.ScheduleRepository.GetByDate(2022, 4);

            schedule.Should().NotBeNull();
            schedule.Days.Count().Should().Be(30);
            schedule.Year.Should().Be(2022);
            schedule.Month.Should().Be(4);
            schedule.IsSaved.Should().BeTrue();
            schedule.Id.GetType().Should().Be(typeof(Guid));
            schedule.NumOfWorkdays.Should().Be(21);
            schedule.Summaries.Should().NotBeNull();
            schedule.Summaries.Count().Should().Be(4);
            schedule.WordFile.Should().NotBeNull();
            schedule.WordFile.FilePath.Should().Be("./");
            schedule.WordFile.FileName.Should().Be("test.docx");
            schedule.WordFile.Id.GetType().Should().Be(typeof(Guid));
            customPublisherMock.Verify(x => x.Publish(It.IsAny<NewScheduleCreatedEvent>(), It.IsAny<PublishStrategy>(), It.IsAny<CancellationToken>()), Times.Once);
            var scheduleDto = _mapper.Map<ScheduleDto>(schedule);
            TestScheduleDays(scheduleDto.Days);
        }

        private void TestScheduleDays(List<Api.Dtos.DayDto> days)
        {
            for (int i = 0; i < days.Count; i++)
            {
                var day = days[i];
                day.Should().NotBeNull();
                day.Date.Should().BeSameDateAs(DateTime.Parse($"2022-04-{i + 1}"));
                day.IsWeekend.Should().Be(IsWeekend(day.Date));
                if (!day.IsHoliday && !day.IsWeekend)
                {
                    day.UsersScheduledForMorning.Should().NotBeEmpty();
                    day.UsersScheduledForMorning.Count.Should().Be(2);
                }
                if (!day.IsHoliday && !day.IsWeekend)
                {
                    day.UsersScheduledForForenoon.Should().NotBeEmpty();
                    day.UsersScheduledForForenoon.Count.Should().Be(2);
                }
                day.UsersOnHoliday.Should().BeEmpty();
            }
        }

        private bool IsWeekend(DateTime date) => date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
    }
}
