using System.Threading.Tasks;

namespace KEC.Notifications.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}