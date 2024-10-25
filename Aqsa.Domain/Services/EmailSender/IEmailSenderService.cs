namespace Aqsa.Domain.Common.Services.EmailSender
{
    public interface IEmailSenderService
    {
        public Task SendEmailAsync(string email, string toName, string subject,string body);
    }
}
