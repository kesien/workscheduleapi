using SendGrid;
using SendGrid.Helpers.Mail;
using WorkScheduleMaker.Helpers;
using WorkScheduleMaker.Models;

namespace WorkScheduleMaker.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly SendGridClient _sendGridClient;
        private readonly EmailClientSettings _settings;
        public EmailService(IConfiguration config)
        {
            _settings = config.GetSection(nameof(EmailClientSettings)).Get<EmailClientSettings>();
            _sendGridClient = new SendGridClient(_settings.ApiKey);
        }
        public async Task SendEmail(List<Dictionary<string, string>> users, string subject, string plaintTextContent, string htmlContent)
        {
            var tos = users.Select(user => new EmailAddress(user.GetValueOrDefault("Email"), user.GetValueOrDefault("Name"))).ToList();
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress(_settings.FromEmail, _settings.FromName), tos, subject, plaintTextContent, htmlContent);
            await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
