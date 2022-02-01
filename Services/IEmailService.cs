using SendGrid;
using WorkScheduleMaker.Models;

namespace WorkScheduleMaker.Services
{
    public interface IEmailService
    {
        Task<Response> SendNewScheduleEmail(string adminId, int year, int month);
        Task<Response> SendScheduleModifiedEmail(string adminId, int year, int month);
    }
}
