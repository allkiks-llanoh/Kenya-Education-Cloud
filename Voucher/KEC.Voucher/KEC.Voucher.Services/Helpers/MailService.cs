using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KEC.Voucher.Services.Helpers
{
    public static class MailService
    {
        private readonly static string _host = ConfigurationManager.AppSettings["MailSmtpHost"];
        private readonly static int _port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        private readonly static bool _enableSSl =bool.Parse(ConfigurationManager.AppSettings["EnableSsl"]);
        private readonly static int _timeOut =int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        private readonly static string _sender = ConfigurationManager.AppSettings["MailSender"];
        private readonly static string _senderPassword = ConfigurationManager.AppSettings["MailSenderPassword"];
        public static async Task SendMail(MailMessage message)
        {
            var client = new SmtpClient
            {
                Port = _port,
                Host = _host,
                EnableSsl = _enableSSl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_sender, _senderPassword),
                Timeout = _timeOut
            };
            await client.SendMailAsync(message);
        }
    }
}
