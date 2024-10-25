using MailKit.Net.Smtp;
using MimeKit;

namespace Aqsa.Domain.Common.Services.EmailSender;

public class EmailSenderService : IEmailSenderService
{
    public async Task SendEmailAsync(string toEmail, string toName, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Aqsa", "abdelgalilhagras@gmail.com"));
        email.To.Add(new MailboxAddress(toName, toEmail));
        email.Subject = subject;

        email.Body = new TextPart("html")
        {
            Text = body
        };

        using (var smtp = new SmtpClient())
        {
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("abdelgalilhagras@gmail.com", "qsxt btnl gock ehar");
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
        }
    }

}

