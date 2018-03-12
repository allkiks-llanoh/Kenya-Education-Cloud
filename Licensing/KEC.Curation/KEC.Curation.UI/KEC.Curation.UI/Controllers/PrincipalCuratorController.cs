using KEC.Curation.UI.ActionFilters;
using KEC.Curation.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
   [CustomAuthorize(Roles = "Principal Curator")]
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
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));

                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,

                };

                return View(chiefCurator);

            }
           
        }
        public ActionResult PrincipalCuratorReverse()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
          

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
