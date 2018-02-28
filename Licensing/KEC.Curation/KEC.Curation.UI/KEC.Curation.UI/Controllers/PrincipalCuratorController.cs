using KEC.Curation.UI.Cors;
using KEC.Curation.UI.Models;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    [Authorize]
    [AllowCrossSiteJson]
    public class PrincipalCuratorController : Controller
    {
        // GET: PrincipalCurator
        public ActionResult PrincipalCurator()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";

            return View();
        }
        public ActionResult PrincipalCuratorReview()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
          
            
                var result = new UserProfileController().GetTokenForApplication();
                


            return View(result);
        }
        public ActionResult get()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";

            return View();
        }
    }
}
