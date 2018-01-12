using KEC.Voucher.Services.AfricasTalking;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
namespace KEC.Voucher.Services.AfricasTalking
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
        public string SendSms(string recipient, string message)
        {
            try
            {
                var sms = _gateway.sendMessage(ProcessNumber(recipient), message);
                return string.Empty;
                //return $"Number: {res["number"]}, Status: {res["status"]},MessageId: {res["messageId"]},Cost: {res["messageId"]}";
              
            }
            catch (AfricasTalkingGatewayException)
            {
                throw;
            }
        }
        private  string ProcessNumber(string number)
        {
            number = Regex.Escape(Convert.ToString(number));
            if (number.StartsWith("0"))
            {
                number.Remove(0, 1);
                number = $"254{number}";
            }
            if (number.StartsWith("7"))
            {
                number = $"254{number}";
            }
            return number;
        }
    }

}
