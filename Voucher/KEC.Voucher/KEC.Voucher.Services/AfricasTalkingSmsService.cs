using System;
using System.Configuration;
using AfricasTalkingCS;
namespace KEC.Voucher.Services
{
    public class AfricasTalkingSmsService
    {
        public readonly string _username;
        private readonly string _apiKey;
        private AfricasTalkingGateway _gateway;
        public AfricasTalkingSmsService()
        {
            _username = ConfigurationManager.AppSettings["ApiUser"];
            _apiKey = ConfigurationManager.AppSettings["ApiKey"];
            _gateway = new AfricasTalkingGateway(_username, _apiKey);


        }
        public void SendSms(string recipient, string message)
        {
            try
            {
                var sms = _gateway.SendMessage($"\"{recipient}\"", $"\"{message}\"");
                var res = sms["SMSMessageData"]["Recipients"];
                foreach (var re in res["SMSMessageData"]["Recipients"])

                {

                    Console.WriteLine((string)re["number"] + ": ");

                    Console.WriteLine((string)re["status"] + ": ");

                    Console.WriteLine((string)re["messageId"] + ": ");

                    Console.WriteLine((string)re["cost"] + ": ");

                }

              
            }
            catch (AfricasTalkingGatewayException)
            {
                throw;
            }
        }
    }
}
