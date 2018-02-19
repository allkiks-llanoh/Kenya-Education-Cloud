using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curatiom.Web.UI.Controllers
{
    public class StagesController : Controller
    {
        // GET: Stages
        public ActionResult Legal()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Verify Legal Requirements";

            return View();
        }
        public ActionResult Finance()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Verify Payment";

            return View();
        }
        public ActionResult LegalVerify()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Verify Legality of Publication";

            return View();
        }
        public ActionResult FinanceVerify()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Verify Payment Has Been Made Against Publication";

            return View();

        }
        public ActionResult Test()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Verify Payment Has Been Made Against Publication";

            return View();
        }
    }
}