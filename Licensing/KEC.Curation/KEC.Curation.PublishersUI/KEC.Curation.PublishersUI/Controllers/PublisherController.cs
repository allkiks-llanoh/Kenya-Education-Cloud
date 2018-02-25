using KEC.Curation.PublishersUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    [Authorize]
    public class PublisherController : Controller
    {

        // GET: Publisher
        public ActionResult Upload()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Upload Publication";
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));

                var publisher = new Publishers
                {
                    Company = user.Company,
                    LastName = user.LastName,
                    guid= user.Id
                };
                return View(publisher);
            }
        }
        public ActionResult Approved()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Approved Publications";
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));

                var publisher = new Publishers
                {
                    Company = user.Company,
                    LastName = user.LastName,
                    guid = user.Id
                };
                return View(publisher);
            }
          
        }
        public ActionResult Conditional()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Approved With Changes Recomended";

            return View();
        }
        public ActionResult Rejected()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Rejected Publication";

            return View();
        }
    }
}