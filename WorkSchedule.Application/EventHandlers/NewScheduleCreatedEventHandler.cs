using MediatR;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using WorkSchedule.Application.Events;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Hubs;
using WorkSchedule.Application.Services.DropboxService;
using WorkSchedule.Application.Services.EmailService;

namespace WorkSchedule.Application.EventHandlers
{
    public class NewScheduleCreatedEventHandler : INotificationHandler<NewScheduleCreatedEvent>
    {
        private readonly IDropboxService _dropBoxService;
        private readonly IEmailService _emailService;
        private readonly IHubContext<ScheduleHub, IHubClient> _hubContext;
        private readonly ILogger _logger;
        public NewScheduleCreatedEventHandler(IDropboxService dropboxService, IEmailService emailService, IHubContext<ScheduleHub, IHubClient> hubContext, ILogger logger)
        {
            _dropBoxService = dropboxService;
            _emailService = emailService;
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task Handle(NewScheduleCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.ScheduleCreatedEvent();
            _logger.Information($"SignalR ScheduleCreatedEvent fired");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), notification.Schedule.WordFile.FilePath);
            using (MemoryStream ms = LoadFile(filePath))
            {
                var result = await _dropBoxService.UploadFile(notification.Schedule.WordFile.FileName, ms);
                if (!result)
                {
                    throw new BusinessException { ErrorCode = 509, ErrorMessages = new List<string> { "Couldn't upload WordFile to Dropbox!" } };
                }
                else
                {
                    _logger.Information($"{notification.Schedule.WordFile.FileName} has been uploaded to DropBox");
                    File.Delete(filePath);
                }
            };

            await _emailService.SendNewScheduleEmail(notification.UserId, notification.Schedule.Year, notification.Schedule.Month);
        }

        private MemoryStream LoadFile(string filePath)
        {
            return new MemoryStream(File.ReadAllBytes(filePath));
        }
    }
}
