using KEC.Curation.UI.ActionFilters;
using KEC.Curation.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    [CustomAuthorize(Roles = "Curator")]
    [AllowCrossSiteJson]
    [UserGuidJson]
    [RoutePrefix("{controller}")]
   
    public class CuratorController : Controller
    {
        // GET: Curator
        [HttpGet,Route("ToCurate")]
        public ActionResult ToCurate(string curatorGuid)
        {
           
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
        [HttpGet,Route("CuratePublication/{Id}")]
        public ActionResult CuratePublication(int Id)
        {
            ViewBag.AssignmentId = Id;
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