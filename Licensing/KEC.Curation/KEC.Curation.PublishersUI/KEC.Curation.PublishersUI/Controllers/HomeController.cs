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
            ViewData["SubTitle"] = "Curation Management System";
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
            ViewData["SubTitle"] = "Curation Management System";
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
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";

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
            ViewData["SubTitle"] = "Curation Management System";
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
            ViewData["SubTitle"] = "Curation Management System";
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
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
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
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
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
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
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
        public ActionResult ChiefCurators()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "Assign To Chief Curators";
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