using System.Threading.Tasks;

namespace KEC.Services.Notifications
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}