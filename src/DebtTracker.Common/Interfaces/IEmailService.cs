using System.Threading.Tasks;

namespace DebtTracker.Common.Interfaces
{
    /// <summary>
    /// Service from sends message on Email.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send messsages in email from SMTP-client.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="subject">Theme</param>
        /// <param name="message">Message</param>
        Task SendEmailAsync(string email, string subject, string message);
    }
}