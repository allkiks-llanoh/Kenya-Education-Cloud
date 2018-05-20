using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Ecommerce.Web.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class StoreController : Controller
    {
        public ActionResult ProductsList()
        {
            return View();
        }

        public ActionResult ProductsGrid()
        {
            return View();
        }

        public ActionResult ProductDetail(int id)
        {
            return View();
        }

        public ActionResult ProductEdit()
        {
            return View();
        }

        public ActionResult Payments()
        {
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

    }
}