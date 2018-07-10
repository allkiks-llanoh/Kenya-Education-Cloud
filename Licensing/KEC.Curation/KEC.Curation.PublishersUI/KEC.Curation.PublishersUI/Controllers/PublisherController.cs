using KEC.Curation.PublishersUI.Models;
using KEC.Curation.PublishersUI.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace KEC.Curation.PublishersUI.Controllers
{
    [AllowCrossSiteJson]
    [Authorize]
    public class PublisherController : Controller
    {
        // GET: Publisher
        public ActionResult Upload()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "Upload Publication";

            using (var context = new ApplicationDbContext())
            {
               
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));

                var publisher = new Publishers
                {
                    Company = user.Company,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    guid= user.Id
                };

               return View(publisher);

            }      
        }
        public ActionResult Approved()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "Approved Publications";
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
        public ActionResult Certificate()
        {
            ViewData["SubTitle"] = "Publishers Certificte";
            ViewData["Message"] = "Certificate";
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
        public ActionResult Conditional()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "Approved With Changes Recomended";

            return View();
        }
        public ActionResult Rejected()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = "Rejected Publication";
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
        public ActionResult PublishToBookStore()
        {
            ViewData["SubTitle"] = "Publishers Portal";
            ViewData["Message"] = " Publish To KEC Book Store";
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