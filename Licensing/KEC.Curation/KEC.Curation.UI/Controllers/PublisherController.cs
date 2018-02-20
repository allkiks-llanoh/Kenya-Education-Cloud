using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    public class PublisherController : Controller
    {
        // GET: Publisher
        public ActionResult Upload()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Upload Publication";

            return View();
        }
        public ActionResult Approved()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Approved Publications";

            return View();
        }
        public ActionResult Conditional()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Approved With Changes Recomended";

            return View();
        }
        public ActionResult Rejected()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Rejected Publication";

            return View();
        }
    }
}