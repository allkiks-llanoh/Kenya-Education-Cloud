﻿using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
  //[Authorize]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SubjectTypes()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Create Subject Type / Category";

            return View();
        }

        public ActionResult Subjects()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Create Subject";

            return View();
        }
        public ActionResult Levels()
        {

            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Create Level";

            return View();

           
        }
       
    }
}