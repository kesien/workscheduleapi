using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using WorkSchedule.Application.Helpers;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SendGridClient _sendGridClient;
        private readonly EmailClientSettings _settings;
        private readonly UserManager<User> _userManager;
        public EmailService(IOptions<EmailClientSettings> options, UserManager<User> userManager)
        {
            _settings = options.Value;
            _sendGridClient = new SendGridClient(_settings.ApiKey);
            _userManager = userManager;
        }

        public async Task<Response> SendNewScheduleEmail(string adminId, int year, int month)
        {
            return await SendEmail(_settings.NewScheduleTemplateId, adminId, year, month);
        }

        private async Task<Response> SendEmail(string templateId, string adminId, int year, int month)
        {
            bool IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            if (IsDevelopment)
            {
                return null;
            }
            var adminName = await GetAdminName(adminId);
            var users = GetUsers();
            var data = new List<object>();
            foreach (var user in users)
            {
                data.Add(new EmailData() { User = user.GetValueOrDefault("Name"), Admin = adminName, Date = $"{year}-{month}" });
            }
            var tos = users.Select(user => new EmailAddress(user.GetValueOrDefault("Email"), user.GetValueOrDefault("Name"))).ToList();
            var msg = MailHelper.CreateMultipleTemplateEmailsToMultipleRecipients(new EmailAddress(_settings.FromEmail, _settings.FromName), tos, templateId, data);
            return await _sendGridClient.SendEmailAsync(msg);
        }

        public async Task<Response> SendScheduleModifiedEmail(string adminId, int year, int month)
        {
            return await SendEmail(_settings.ScheduleModifiedTemplateId, adminId, year, month);
        }

        private List<Dictionary<string, string>> GetUsers()
        {
            var users = _userManager.Users.ToList();
            return users.Select(user => new Dictionary<string, string>()
            {
                { "Email", user.UserName },
                { "Name", user.Name }
            }).ToList();
        }

        private async Task<string> GetAdminName(string id)
        {
            var admin = await _userManager.FindByIdAsync(id);
            return admin.Name;
        }
    }
}
