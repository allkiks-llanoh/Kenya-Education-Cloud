using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    public class CurationManagersController : Controller
    {
        // GET: CurationManagers

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet, Route("ViewPublication/{Id:int}")]
        public ActionResult ViewPublication(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
            return View();
        }
       
        public ActionResult ReviewPublication(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
            return View();
        }
        public ActionResult DetailApproved(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
            return View();
        }
        public ActionResult ChiefCurators()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
            return View();
        }
        public ActionResult PrincipalCurators()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
            return View();
        }
        public ActionResult AllPublications()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "View All Publications";

            return View();
        }
        public ActionResult Approved()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "View Approved Publications";

            return View();
        }
        public ActionResult PartiallyApproved()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";

            return View();
        }
        public ActionResult Pending()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "View Pending Publications";
            return View();
        }
        public ActionResult Rejected()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "View Rejected Publications";

            return View();
        }
    }
}
