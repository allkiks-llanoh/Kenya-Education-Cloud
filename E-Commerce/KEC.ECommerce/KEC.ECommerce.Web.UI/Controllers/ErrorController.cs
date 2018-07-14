using Microsoft.AspNetCore.Mvc;

namespace KEC.ECommerce.Web.UI.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("500")]
        public IActionResult AppError()
        {
            return View();
        }
        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}