using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    [RoutePrefix("{controller}")]
    public class ChiefCuratorController : Controller
    {
       
        // GET: ChiefCurator
        public ActionResult Publications()
        {
            return View();
        }
        [HttpGet,Route("ViewPublication/{Id:int}")]
        public ActionResult ViewPublication(int Id)
        {
            ViewBag.PublicationId = Id;
            return View();
        }
        [HttpGet, Route("AssignPublication/{id:int}")]
        public ActionResult AssignPublication(int Id)
        {
            ViewBag.PublicationId = Id;
            return View();
        }
    }
}