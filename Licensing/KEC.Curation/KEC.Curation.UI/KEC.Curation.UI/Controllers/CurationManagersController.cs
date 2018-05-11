using KEC.Curation.UI.ActionFilters;
using KEC.Curation.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    [CustomAuthorize(Roles = "Curation Manager,  Admin")]
    [UserGuidJson]
    [AllowCrossSiteJson]
    public class CurationManagersController : Controller
    {
        // GET: CurationManagers

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
        [HttpGet, Route("ViewPublication/{Id:int}")]
        public ActionResult ViewPublication(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "";
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
       
        public ActionResult ReviewPublication(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "";
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
        public ActionResult DetailApproved(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "";
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
        public ActionResult Curators()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "";
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
        public ActionResult ChiefCurators()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "";
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
        public ActionResult PrincipalCurators()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "";
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
        public ActionResult AllPublications()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "View All Publications";

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
        public ActionResult Approved()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "View Approved Publications";

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
        public ActionResult PartiallyApproved()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "";

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
        public ActionResult Pending()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "View Pending Publications";
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
        public ActionResult Rejected()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "View Not Approved Publications";

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
