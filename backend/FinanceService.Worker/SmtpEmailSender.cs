using System.Net;
using System.Net.Mail;

namespace FinanceService.Worker;

public class SmtpEmailSender(SmtpEmailSettings settings) : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        var message = new MailMessage
        {
            From = new MailAddress(settings.FromAddress, settings.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };

        message.To.Add(new MailAddress(to));

        using var client = new SmtpClient(settings.Host, settings.Port);
        client.Credentials = new NetworkCredential(settings.Username, settings.Password);
        client.EnableSsl = settings.EnableSsl;

        await client.SendMailAsync(message);
    }
}