using SendGrid;

namespace WorkSchedule.Application.Services.EmailService
{
    public interface IEmailService
    {
        Task<Response> SendNewScheduleEmail(Guid adminId, int year, int month);
        Task<Response> SendScheduleModifiedEmail(Guid adminId, int year, int month);
    }
}
