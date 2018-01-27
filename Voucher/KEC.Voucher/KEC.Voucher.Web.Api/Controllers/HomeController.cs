using KEC.Voucher.Services.AfricasTalking;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KEC.Voucher.Web.Api.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
    }
}
