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
            ViewData["SubTitle"] = "Curation Management System";
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
            ViewData["SubTitle"] = "Curation Management System";
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