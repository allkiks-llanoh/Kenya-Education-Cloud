using KEC.Curation.UI.ActionFilters;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
   // [CustomAuthorize(Roles = "Principal Curator")]
    [UserGuidJson]
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
           

            
                //var result = new UserProfileController().GetTokenForApplication();
                


            return View();
        }
        public ActionResult get()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";

            return View();
        }
    }
}
