using MediatR;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using WorkSchedule.Application.Events;
using WorkSchedule.Application.Hubs;
using WorkSchedule.Application.Services.DropboxService;

namespace WorkSchedule.Application.EventHandlers
{
    public class ScheduleDeletedEventHandler : INotificationHandler<ScheduleDeletedEvent>
    {
        private readonly IDropboxService _dropboxService;
        private readonly IHubContext<ScheduleHub, IHubClient> _hubContext;
        private readonly ILogger _logger;

        public ScheduleDeletedEventHandler(IDropboxService dropboxService, IHubContext<ScheduleHub, IHubClient> hubContext, ILogger logger)
        {
            _dropboxService = dropboxService;
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Handle(ScheduleDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.ScheduleDeletedEvent();
            _logger.Information($"SignalR ScheduleDeletedEvent has been fired");
            if (notification.Schedule.WordFile is not null)
            {
                await _dropboxService.DeleteFile(notification.Schedule.WordFile.FileName);
                _logger.Information($"{notification.Schedule.WordFile.FileName} has been deleted from DropBox");
            }
        }
    }
}
