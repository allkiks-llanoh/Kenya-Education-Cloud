using KEC.Curation.UI.ActionFilters;
using KEC.Curation.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
        
    public class StagesController : Controller
    {
        // GET: Stages
        [AllowCrossSiteJson]
        [UserGuidJson]
        [CustomAuthorize(Roles = "Verification office")]
        public ActionResult Legal()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Legal Requirements";

            return View();
        }
        [AllowCrossSiteJson]
        [UserGuidJson]
        [CustomAuthorize(Roles = "Finance Office")]
        public ActionResult Finance()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Payment";

            return View();
        }
        [AllowCrossSiteJson]
        [UserGuidJson]
        [CustomAuthorize(Roles = "Verification office")]
        public ActionResult LegalVerify()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Legality of Publication";

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId
                };
                return View(chiefCurator);
            }
        }
        [AllowCrossSiteJson]
        [UserGuidJson]
        [CustomAuthorize(Roles = "Finance Office")]
        public ActionResult FinanceVerify()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Verify Payment Has Been Made Against Publication";

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId
                };
                return View(chiefCurator);
            }

        }
      
    }
}