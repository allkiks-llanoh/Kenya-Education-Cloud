using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    [RoutePrefix("chiefcurator")]
    public class ChiefCuratorController : Controller
    {
        // GET: ChiefCurator
        public ActionResult Index()
        {
            return View();
        }
       
    }
}