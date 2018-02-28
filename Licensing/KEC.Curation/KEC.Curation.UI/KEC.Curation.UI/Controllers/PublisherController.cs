using KEC.Curation.UI.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    public class PublisherController : Controller
    {
        [Authorize]
        [AllowCrossSiteJson]
        // GET: Publisher
        public ActionResult Upload()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Upload Publication";

            return View();
        }
        public ActionResult Approved()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Approved Publications";

            return View();
        }
        public ActionResult Conditional()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Approved With Changes Recomended";

            return View();
        }
        public ActionResult Rejected()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Rejected Publication";

            return View();
        }
    }
}