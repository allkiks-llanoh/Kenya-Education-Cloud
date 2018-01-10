using KEC.Voucher.Services;
using KEC.Voucher.Web.Api.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KEC.Voucher.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var list = new List<string>();
            ViewBag.Title = "Home";
         
            return View();
        }
    }
}
