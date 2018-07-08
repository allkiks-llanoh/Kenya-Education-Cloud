using KEC.ECommerce.Web.UI.Mailer;

namespace KEC.ECommerce.Web.UI.Helpers
{
    public class AccountActions
    {
        public static void SendEmail(EmailMessage emailMessage, EmailService emailService,EmailConfiguration emailConfiguration)
        {

            emailService.Send(emailMessage, emailConfiguration);

        }
    }
}
