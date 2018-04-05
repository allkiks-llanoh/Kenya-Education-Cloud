using KEC.Curation.UI;
using KEC.Curation.UI.ActionFilters;
using KEC.Curation.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace KEC.Curatiom.Web.UI.Controllers
{
    [CustomAuthorize(Roles = "Chief Curator")]
    [UserGuidJson]
    [AllowCrossSiteJson]
    [RoutePrefix("{controller}")]
   
    public class ChiefCuratorController : Controller
    {
       
        // GET: ChiefCurator
        public ActionResult Publications()
        {
            ViewBag.ViewPublicationUrl = Url.Action("ViewPublication");
            ViewBag.AssignPublicationUrl = Url.Action("AssignPublication");
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid= user.SubjectId
                };
                return View(chiefCurator);
            }
        }
            
        [HttpGet,Route("ViewPublication/{Id:int}")]
        public ActionResult ViewPublication(int Id)
        {
            ViewBag.PublicationId = Id;
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
        [HttpGet, Route("AssignPublication/{id:int}")]
        public ActionResult AssignPublication(int Id)
        {
            ViewBag.PublicationId = Id;

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
        [HttpGet]
        public ActionResult ChiefCuratorView()
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
        [HttpGet]
        public ActionResult ChiefCuratorComments()
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
        [HttpGet, Route("PublicationHistory/{id:int}")]
        public ActionResult PublicationHistory(int Id)
        {
            ViewBag.PublicationId = Id;

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