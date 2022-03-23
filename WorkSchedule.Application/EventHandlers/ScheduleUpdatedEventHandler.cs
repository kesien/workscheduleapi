using Microsoft.AspNetCore.SignalR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Application.Events;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Hubs;
using WorkSchedule.Application.Services.DropboxService;
using WorkSchedule.Application.Services.EmailService;

namespace WorkSchedule.Application.EventHandlers
{
    public class ScheduleUpdatedEventHandler
    {
        private readonly IDropboxService _dropBoxService;
        private readonly IEmailService _emailService;
        private readonly IHubContext<ScheduleHub, IHubClient> _hubContext;
        private readonly ILogger _logger;
        public ScheduleUpdatedEventHandler(IDropboxService dropBoxService, IEmailService emailService, IHubContext<ScheduleHub, IHubClient> hubContext, ILogger logger)
        {
            _emailService = emailService;
            _hubContext = hubContext;
            _logger = logger;
            _dropBoxService = dropBoxService;
        }
        public async Task Handle(ScheduleUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.ScheduleUpdatedEvent();
        _logger.Information($"SignalR ScheduleUpdatedEvent fired");
        if (notification.Schedule.WordFile is not null)
        {
            await _dropBoxService.DeleteFile(notification.Schedule.WordFile.FileName);
            _logger.Information($"{notification.Schedule.WordFile.FileName} has been deleted from DropBox");
        }
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

        //await _emailService.SendNewScheduleEmail(notification.UserId, notification.Schedule.Year, notification.Schedule.Month);
    }

    private MemoryStream LoadFile(string filePath)
    {
        return new MemoryStream(File.ReadAllBytes(filePath));
    }
}
}
