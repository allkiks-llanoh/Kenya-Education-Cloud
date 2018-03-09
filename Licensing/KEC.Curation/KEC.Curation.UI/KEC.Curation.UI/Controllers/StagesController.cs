using KEC.Curation.UI.ActionFilters;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
        [AllowCrossSiteJson]
        [UserGuidJson]
    public class StagesController : Controller
    {


        // GET: Stages
        [CustomAuthorize(Roles = "Verification office")]
        public ActionResult Legal()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Legal Requirements";

            return View();
        }
        [CustomAuthorize(Roles = "Finance Office")]
        public ActionResult Finance()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Payment";

            return View();
        }
        [CustomAuthorize(Roles = "Verification office")]
        public ActionResult LegalVerify()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Legality of Publication";

            return View();
        }
        [CustomAuthorize(Roles = "Finance Office")]
        public ActionResult FinanceVerify()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Payment Has Been Made Against Publication";

            return View();

        }
      
    }
}