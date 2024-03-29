﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DataSaturdaysWebsite.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.
        private IConfiguration Configuration { get; }

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger, IConfiguration configuration)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
            Configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            Options.SendGridKey = Configuration["SendGrid:APIKey"];

            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(Options.SendGridKey, subject, message, toEmail);
        }
        
        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);

            //var sender = Configuration["AppSettings: AppEmail"];

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("support@datasaturdays.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}
