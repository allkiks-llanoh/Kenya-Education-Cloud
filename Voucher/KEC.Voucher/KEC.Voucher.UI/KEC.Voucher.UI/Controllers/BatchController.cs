using KEC.Voucher.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Voucher.UI.Controllers
{
    public class BatchController : Controller
    {
        // GET: Batch
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
        public ActionResult CreateBatch()
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
        public ActionResult ManageBatch()
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
    }
}