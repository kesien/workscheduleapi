using SendGrid;

namespace WorkSchedule.Application.Services.EmailService
{
    public interface IEmailService
    {
        Task<Response> SendNewScheduleEmail(string adminId, int year, int month);
        Task<Response> SendScheduleModifiedEmail(string adminId, int year, int month);
    }
}
