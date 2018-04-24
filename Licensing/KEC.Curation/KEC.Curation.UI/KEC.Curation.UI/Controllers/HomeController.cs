using KEC.Curation.UI.ActionFilters;
using KEC.Curation.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    
    [UserGuidJson]
    [AllowCrossSiteJson]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName = user.FullName
                };
                return View(chiefCurator);
            }
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult SubjectTypes()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Create Subject Type / Category";

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName = user.FullName
                };
                return View(chiefCurator);
            }
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Subjects()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Create Subject";

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName  = user.FullName
                };
                return View(chiefCurator);
            }
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Levels()
        {

            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Create Level";

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName = user.FullName
                };
                return View(chiefCurator);
            }


        }
    }
}