using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace KEC.Notifications.Services
{
    public class EmailSender: IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                //From Address  
                string FromAddress = "noreply@miyabiafrica.com";
                string FromAdressTitle = "KEC Notifications";
                //To Address  
                string ToAddress = email;
                string ToAdressTitle = "KEC Notification";
                string Subject = subject;
                string BodyContent = message;

                //Smtp Server  
                string SmtpServer = "smtp.office365.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                (FromAdressTitle,
                    FromAddress
                ));
                mimeMessage.To.Add(new MailboxAddress
                (ToAdressTitle,
                    ToAddress
                ));
                mimeMessage.Subject = Subject; //Subject
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate(
                        "noreply@miyabiafrica.com",
                        "$9(pk7&u1ASms"
                    );
                    await client.SendAsync(mimeMessage);
                    Console.WriteLine("The mail has been sent successfully !!");
                    Console.ReadLine();
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SendEmailConfirmAsync(string email, string message)
        {
            try
            {
                //From Address  
                string FromAddress = "noreply@miyabiafrica.com";
                string FromAdressTitle = "KEC Notifications";
                //To Address  
                string ToAddress = email;
                string ToAdressTitle = "KEC Notification";
                string Subject = "Trial";
                string BodyContent = message;

                //Smtp Server  
                string SmtpServer = "smtp.office365.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                (FromAdressTitle,
                    FromAddress
                ));
                mimeMessage.To.Add(new MailboxAddress
                (ToAdressTitle,
                    ToAddress
                ));
                mimeMessage.Subject = Subject; //Subject
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate(
                        "noreply@miyabiafrica.com",
                        "$9(pk7&u1ASms"
                    );
                    await client.SendAsync(mimeMessage);
                    Console.WriteLine("The mail has been sent successfully !!");
                    Console.ReadLine();
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            throw new NotImplementedException();
        }
    }
}
