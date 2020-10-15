using DebtTracker.Common.Constants;
using DebtTracker.Common.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace DebtTracker.BLL.Services
{
    /// <inheritdoc cref="IEmailService<T>"/>
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта DebtTracker", EmailConstants.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(EmailConstants.SMTPString, EmailConstants.Port, false);
                await client.AuthenticateAsync(EmailConstants.SenderEmail, EmailConstants.PasswordEmail);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
