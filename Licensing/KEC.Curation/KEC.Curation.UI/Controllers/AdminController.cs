using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Schooltypes()
        {
            ViewData["SubTitle"] = "Kenya Education Cloud";
            ViewData["Message"] = "Create School Types";

            return View();
        }
    }
}