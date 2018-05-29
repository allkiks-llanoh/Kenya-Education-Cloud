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

        public ActionResult AllPublications()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "View All Publications";
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
        public ActionResult Approved()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "View Approved Publications";

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
        public ActionResult PartiallyApproved()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "Publication Details";

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
        public ActionResult Pending()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "View Pending Publications";
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
        public ActionResult Rejected()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "View Rejected Publications";

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
        
        public ActionResult ViewPublication(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "Publication Details";
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

        public ActionResult ReviewPublication(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "Review Publication";
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
        public ActionResult DetailApproved(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "Publication Details";
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
        public ActionResult DetailReject(int Id)
        {
            ViewBag.PublicationId = Id;
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "Publication Details";
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
       
    }
}