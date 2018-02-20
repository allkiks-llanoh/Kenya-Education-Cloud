using System.Diagnostics;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }
    }
}