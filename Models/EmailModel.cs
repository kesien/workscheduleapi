﻿using SendGrid.Helpers.Mail;

namespace WorkScheduleMaker.Models
{
    public class Email
    {
        public EmailAddress From { get; set; }
        public EmailAddress To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
