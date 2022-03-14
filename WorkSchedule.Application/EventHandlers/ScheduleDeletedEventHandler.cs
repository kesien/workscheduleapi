using MediatR;
using Microsoft.AspNetCore.SignalR;
using WorkSchedule.Application.Events;
using WorkSchedule.Application.Hubs;
using WorkSchedule.Application.Services.DropboxService;

namespace WorkSchedule.Application.EventHandlers
{
    public class ScheduleDeletedEventHandler : INotificationHandler<ScheduleDeletedEvent>
    {
        private readonly IDropboxService _dropboxService;
        private readonly IHubContext<ScheduleHub, IHubClient> _hubContext;

        public ScheduleDeletedEventHandler(IDropboxService dropboxService, IHubContext<ScheduleHub, IHubClient> hubContext)
        {
            _dropboxService = dropboxService;
            _hubContext = hubContext;
        }

        public async Task Handle(ScheduleDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.ScheduleDeletedEvent();
            if (notification.Schedule.WordFile is not null)
            {
                await _dropboxService.DeleteFile(notification.Schedule.WordFile.FileName);
            }
        }
    }
}
