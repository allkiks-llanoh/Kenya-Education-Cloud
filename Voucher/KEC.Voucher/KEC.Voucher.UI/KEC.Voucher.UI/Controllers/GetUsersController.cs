using KEC.Voucher.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Voucher.UI.Controllers
{
    public class GetUsersController : Controller
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();
        // GET: GetUsers
        public ActionResult GetAllUsers()
        {
            var _users = context.Users.ToList();

            return Json(new SelectList(_users, "FullName", "Email"));
        }
    }
}