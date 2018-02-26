﻿using KEC.Curation.UI.Cors;
using System.Web.Mvc;

namespace KEC.Curatiom.Web.UI.Controllers
{
    [AllowCrossSiteJson]
    [RoutePrefix("{controller}")]
    [Authorize]
    public class ChiefCuratorController : Controller
    {
       
        // GET: ChiefCurator
        public ActionResult Publications()
        {
           
            ViewBag.ViewPublicationUrl = Url.Action("ViewPublication");
            ViewBag.AssignPublicationUrl = Url.Action("AssignPublication");
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
        [HttpGet, Route("PublicationHistory/{id:int}")]
        public ActionResult PublicationHistory(int Id)
        {
            ViewBag.PublicationId = Id;
            return View();
        }
    }
}