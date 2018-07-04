﻿using KEC.Voucher.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Voucher.UI.Controllers
{
    //[CustomAuthorize(Roles = "School Admin")]
    public class ClientController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var voucherUser = new VoucherUser
                {
                    Guid = user.Id,
                    FullName = user.FullName,
                    Email = user.UserName
                    
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
                    FullName = user.FullName,
                    Email = user.UserName
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