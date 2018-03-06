using KEC.Curation.UI.ActionFilters;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    [Authorize]
    [AllowCrossSiteJson]
    [UserGuidJson]
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