using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class PrincipalCuratorController : Controller
    {
        // GET: PrincipalCurator
        public ActionResult PrincipalCurator()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Assign To Chief Curators";

            return View();
        }
        public ActionResult PrincipalCuratorReview()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Assign To Chief Curators";

            return View();
        }
        public ActionResult get()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Assign To Chief Curators";

            return View();
        }
    }
}
