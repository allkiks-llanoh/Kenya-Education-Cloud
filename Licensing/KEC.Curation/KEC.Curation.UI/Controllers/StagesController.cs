using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    public class StagesController : Controller
    {
      [Authorize]
        // GET: Stages
        public ActionResult Legal()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Legal Requirements";

            return View();
        }
        public ActionResult Finance()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Payment";

            return View();
        }
        public ActionResult LegalVerify()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Legality of Publication";

            return View();
        }
        public ActionResult FinanceVerify()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Payment Has Been Made Against Publication";

            return View();

        }
        public ActionResult Test()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Payment Has Been Made Against Publication";

            return View();
        }
    }
}