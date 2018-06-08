using System.Threading.Tasks;

namespace KEC.Notifications.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}