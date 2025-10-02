using ERP.Application.Interfaces.Email;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ERP.Infrastructure.ExternalServices.Email;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendEmail(EmailDto email)
    {
        try
        {
            var message = new MimeMessage();
            var from = new MailboxAddress("LeaveManagement", "asefi.test@gmail.com");
            message.From.Add(from);

            var To = new MailboxAddress("User", email.Destination);
            message.To.Add(To);

            message.Subject = email.Title;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = email.MessageBody
            };

            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("asefi.test@gmail.com", "uqgj vhwa zdvu hdxn");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            client.Dispose();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error in sending email");
            return false;
        }
    }
}