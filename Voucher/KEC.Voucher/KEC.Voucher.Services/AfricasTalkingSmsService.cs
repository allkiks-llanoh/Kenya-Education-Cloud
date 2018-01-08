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
        public string SendSms(string recipient, string message)
        {
            try
            {
                var sms = _gateway.SendMessage(recipient, message);
                var res = sms["SMSMessageData"]["Recipients"][0];
                return $"Number: {res["number"]},Status:{res["status"]},MessageId: {res["messageId"]},Cost: {res["cost"]}";
            }
            catch (AfricasTalkingGatewayException)
            {
                throw;
            }
        }
    }
}
