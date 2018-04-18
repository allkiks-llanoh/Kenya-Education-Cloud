using KEC.Curation.PublishersUI.Cors;
using KEC.Curation.PublishersUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.PublishersUI.Controllers
{
    [AllowCrossSiteJson]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {

                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));

                var publisher = new Publishers
                {
                    Company = user.Company,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    guid = user.Id
                };

                return View(publisher);

            }
        }

        public ActionResult About()
        {
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}