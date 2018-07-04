﻿using KEC.Voucher.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace KEC.Voucher.UI.Controllers
{
    public class SchoolController : Controller
    {
        [CustomAuthorize(Roles = "Schools Creator")]
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var voucherUser = new VoucherUser
                {
                    Guid = user.Id,
                    FullName = user.FullName
                };
                return View(voucherUser);
            }
        }
       
        public ActionResult Transaction()
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var voucherUser = new VoucherUser
                {
                    Guid = user.Id,
                    FullName = user.FullName
                };
                return View(voucherUser);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}