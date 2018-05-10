using KEC.Curation.UI.ActionFilters;
using KEC.Curation.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    [CustomAuthorize(Roles = "Curator")]
    [AllowCrossSiteJson]
    [UserGuidJson]
   
    public class CuratorController : Controller
    {
        public ActionResult Curator()
        {
            ViewData["SubTitle"] = "Assigned Publications";
            ViewData["Message"] = " ";
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
        public ActionResult List()
        {
            ViewData["SubTitle"] = "Assigned Publications";
            ViewData["Message"] = " ";
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
        public ActionResult ToCurate(string curatorGuid)
        {
           
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName= user.FullName
                };
                return View(chiefCurator);
            }
        }
       
        public ActionResult CuratorView(string userguidGuid)
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
  
        public ActionResult CuratePublication(int Id)
        {
            ViewBag.AssignmentId = Id;
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