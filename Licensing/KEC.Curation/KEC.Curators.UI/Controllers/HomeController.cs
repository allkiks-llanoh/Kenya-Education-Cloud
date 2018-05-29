using KEC.Curators.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace KEC.Curators.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var _curator = new curator
                {
                    Guid = user.Id,
                    FullName = user.UserName
                };
                return View(_curator);
            }
        }

        public ActionResult Curator()
        {
            ViewData["SubTitle"] = "Assigned Publications";
            ViewData["Message"] = " ";
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var _curator = new curator
                {
                    Guid = user.Id,
                    FullName = user.UserName
                };
                return View(_curator);
            }

        }

        public ActionResult CuratorView(string userguidGuid)
        {
            ViewData["SubTitle"] = "Assigned Publications";
            ViewData["Message"] = " ";
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var _curator = new curator
                {
                    Guid = user.Id,
                    FullName = user.UserName
                };
                return View(_curator);
            }

        }
    }
}