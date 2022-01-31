using WorkScheduleMaker.Models;

namespace WorkScheduleMaker.Services
{
    public interface IEmailService
    {
        Task SendEmail(List<Dictionary<string, string>> users, string subject, string plaintTextContent, string htmlContent);
    }
}
