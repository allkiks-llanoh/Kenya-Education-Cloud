using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace KEC.Services.Notifications
{
    public class EmailSender: IEmailSender, ISmsSender
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

        public async Task SendEmailConfirmationAsync(string email, string callbackUrl)
        {
            try
            {
                //From Address  
                string FromAddress = "noreply@miyabiafrica.com";
                string FromAdressTitle = "KEC Notifications";
                //To Address  
                string ToAddress = email;
                string ToAdressTitle = "KEC Notifications";
                string Subject = "KEC Notifications";
                string BodyContent = callbackUrl;

                //Smtp Server  
                string SmtpServer = "smtp.office365.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;
                var concatingText = "Confirm your email ,Please confirm your account by clicking this link:";
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
                   
                    Text = $"{concatingText}{BodyContent}"
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate(
                        "noreply@miyabiafrica.com",
                        "$9(pk7&u1ASms"
                    );
                    await client.SendAsync(mimeMessage);
                    //    Console.WriteLine("The mail has been sent successfully !!");
                    //    Console.ReadLine();
                    //    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SendTaskNotification(string email, string assignedTask)
        {
            try
            {
                //From Address  
                string FromAddress = "noreply@miyabiafrica.com";
                string FromAdressTitle = "KEC Notifications";
                //To Address  
                string ToAddress = email;
                string ToAdressTitle = "KEC Notification";
                string Subject = "KEC Notifications";
                string BodyContent = assignedTask;

                //Smtp Server  
                string SmtpServer = "smtp.office365.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;
                var concatingText = "You Have Recieved New Publication on Curation Management System:";
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

                    Text = $"{concatingText}{BodyContent}"
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate(
                        "noreply@miyabiafrica.com",
                        "$9(pk7&u1ASms"
                    );
                    await client.SendAsync(mimeMessage);
                    //    Console.WriteLine("The mail has been sent successfully !!");
                    //    Console.ReadLine();
                    //    await client.DisconnectAsync(true);
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
