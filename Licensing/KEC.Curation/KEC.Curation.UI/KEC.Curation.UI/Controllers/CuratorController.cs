using KEC.Curation.UI.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    [AllowCrossSiteJson]
    [RoutePrefix("{controller}")]
    public class CuratorController : Controller
    {
        // GET: Curator
        [HttpGet,Route("ToCurate")]
        public ActionResult ToCurate(string curatorGuid)
        {
            ViewBag.CurateUrl = Url.Action("CuratePublication");
            return View();
        }
        [HttpGet,Route("CuratePublication/{Id}")]
        public ActionResult CuratePublication(int Id)
        {
            ViewBag.AssignmentId = Id;
            return View();
        }
    }
}