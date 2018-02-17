using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult SubjectTypes()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Create Subject Type / Category";

            return View();
        }

        public ActionResult Subjects()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Create Subject";

            return View();
        }
        public ActionResult Levels()
        {

            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Create Level";

            return View();

           
        }
       
    }
}