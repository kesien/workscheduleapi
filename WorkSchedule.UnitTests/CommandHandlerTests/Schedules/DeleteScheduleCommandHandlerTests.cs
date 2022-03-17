using FluentAssertions;
using Moq;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Application.CommandHandlers.Schedules;
using WorkSchedule.Application.Constants;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Events;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Helpers;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.Services.ScheduleService;
using WorkSchedule.UnitTests.Data;
using WorkSchedule.UnitTests.MockServices;
using Xunit;

namespace WorkSchedule.UnitTests.CommandHandlerTests.Schedules
{
    public class DeleteScheduleCommandHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _context;
        private readonly IScheduleService _scheduleService;
        private readonly ILogger _logger;
        public DeleteScheduleCommandHandlerTests()
        {
            var dp = new DataProvider();
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
        public async Task ValidId_Should_BeDeleted()
        {
            var customPublisherMock = new Mock<ICustomPublisher>();
            customPublisherMock.Setup(x => x.Publish(It.IsAny<ScheduleDeletedEvent>(), It.IsAny<PublishStrategy>(), It.IsAny<CancellationToken>())).Verifiable();
            await _scheduleService.CreateSchedule(2022, 5);
            var schedule = await _uow.ScheduleRepository.GetByDate(2022, 5);
            schedule.Should().NotBeNull();
            
            var commandHandler = new DeleteScheduleCommandHandler(_scheduleService, customPublisherMock.Object, _logger);
            var command = new DeleteScheduleCommand { Id = schedule.Id };


            await commandHandler.Handle(command, CancellationToken.None);
            schedule = await _uow.ScheduleRepository.GetByDate(2022, 5);
            schedule.Should().BeNull();
            customPublisherMock.Verify(x => x.Publish(It.IsAny<ScheduleDeletedEvent>(), It.IsAny<PublishStrategy>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task InValidId_Should_ThrowError()
        {
            var customPublisherMock = new Mock<ICustomPublisher>();
            customPublisherMock.Setup(x => x.Publish(It.IsAny<ScheduleDeletedEvent>(), It.IsAny<PublishStrategy>(), It.IsAny<CancellationToken>())).Verifiable();

            var commandHandler = new DeleteScheduleCommandHandler(_scheduleService, customPublisherMock.Object, _logger);
            var command = new DeleteScheduleCommand { Id = Guid.Empty };


            await commandHandler.Awaiting(c => c.Handle(command, CancellationToken.None)).Should().ThrowAsync<BusinessException>().Where(e => e.ErrorMessages.Count == 1);
            customPublisherMock.Verify(x => x.Publish(It.IsAny<ScheduleDeletedEvent>(), It.IsAny<PublishStrategy>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
