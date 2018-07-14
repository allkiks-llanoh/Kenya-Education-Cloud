namespace KEC.ECommerce.Web.UI.Mailer
{
    public interface IEmailService
    {
        void Send(EmailMessage message,EmailConfiguration emailConfiguration);
    }
}
