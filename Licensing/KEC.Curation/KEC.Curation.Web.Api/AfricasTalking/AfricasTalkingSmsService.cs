using KEC.Curation.Services.AfricasTalking;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
namespace KEC.Curation.Services.AfricasTalking
{
    public class AfricasTalkingSmsService
    {
        public readonly string _username;
        private readonly string _apiKey;
        private AfricasTalkingGateway _gateway;
        public AfricasTalkingSmsService()
        {
            _username = "Miyabi";
            _apiKey = "b35d794f01c9382f51ef5959169f70d5319de7369921f5e47bd2c9138e624aeb";
            _gateway = new AfricasTalkingGateway(_username, _apiKey);


        }
         public void SendSms(string recipient, string message)
        {
            try
            {
               _gateway.sendMessage(ProcessNumber(recipient), message);
                
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
                number = $"+254{number}";
            }
            if (number.StartsWith("7"))
            {
                number = $"+254{number}";
            }
            if (number.StartsWith("+"))
            {
                number=number.Remove(0, 1);
            }
            return number;
        }
    }

}
