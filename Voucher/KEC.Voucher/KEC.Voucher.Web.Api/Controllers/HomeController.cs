using KEC.Voucher.Services.AfricasTalking;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KEC.Voucher.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var list = new List<string>();
            ViewBag.Title = "Home";
            var smsService = new AfricasTalkingSmsService();
            smsService.SendSms("+254711861170","This is a test");
            return View();
        }
    }
}
