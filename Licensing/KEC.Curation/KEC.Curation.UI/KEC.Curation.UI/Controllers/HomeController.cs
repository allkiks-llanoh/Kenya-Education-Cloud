using KEC.Curation.UI.ActionFilters;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    
    [UserGuidJson]
    [AllowCrossSiteJson]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
           
            return View();
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult SubjectTypes()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Create Subject Type / Category";

            return View();
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Subjects()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Create Subject";

            return View();
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Levels()
        {

            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Create Level";

            return View();


        }
    }
}