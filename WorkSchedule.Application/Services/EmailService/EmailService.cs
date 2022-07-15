using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Helpers;

namespace WorkSchedule.Application.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SendGridClient _sendGridClient;
        private readonly EmailClientSettings _settings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public EmailService(IOptions<EmailClientSettings> options, ILogger logger, IUnitOfWork unitOfWork)
        {
            _settings = options.Value;
            _sendGridClient = new SendGridClient(_settings.ApiKey);
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> SendNewScheduleEmail(Guid adminId, int year, int month)
        {
            return await SendEmail(_settings.NewScheduleTemplateId, adminId, year, month);
        }

        private async Task<Response> SendEmail(string templateId, Guid adminId, int year, int month)
        {
            bool IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            if (IsDevelopment)
            {
                return null;
            }
            var adminName = await GetAdminName(adminId);
            var users = await GetUsers();
            var data = new List<object>();
            foreach (var user in users)
            {
                data.Add(new EmailData() { User = user.GetValueOrDefault("Name"), Admin = adminName, Date = $"{year}-{month}" });
            }
            var tos = users.Select(user => new EmailAddress(user.GetValueOrDefault("Email"), user.GetValueOrDefault("Name"))).ToList();
            var msg = MailHelper.CreateMultipleTemplateEmailsToMultipleRecipients(new EmailAddress(_settings.FromEmail, _settings.FromName), tos, templateId, data);
            _logger.Information($"E-mail with template: {templateId} has been sent to {users.Count} users");
            return await _sendGridClient.SendEmailAsync(msg);
        }

        public async Task<Response> SendScheduleModifiedEmail(Guid adminId, int year, int month)
        {
            return await SendEmail(_settings.ScheduleModifiedTemplateId, adminId, year, month);
        }

        private async Task<List<Dictionary<string, string>>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.Get(user => user.ReceiveEmails && user.Role != Constants.UserRole.SUPERADMIN);
            return users.Where(user => user.ReceiveEmails).Select(user => new Dictionary<string, string>()
            {
                { "Email", user.UserName },
                { "Name", user.Name }
            }).ToList();
        }

        private async Task<string> GetAdminName(Guid id)
        {
            var admin = await _unitOfWork.UserRepository.GetByID(id);
            return admin.Name;
        }
    }
}
