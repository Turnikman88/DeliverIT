using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class MailService : IMailService
    {
        private readonly IMailSettings _mailSettings;
        public MailService(IMailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendEmailAsync(MailDTO mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(_mailSettings.Mail));            
            email.Subject = mailRequest.Subject ?? "";

            var builder = new BodyBuilder();
            builder.TextBody = $"From: '{mailRequest.Name}' Email: '{mailRequest.Email}':{Environment.NewLine}{mailRequest.Message}";
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

    }
}
