using Authentication_Test.Models;
using KEC.Curation.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Authentication_Test.Controllers
{
    public class GetUsersController : Controller
    {
        private readonly ApplicationDbContext  context = new ApplicationDbContext();
        // GET: GetUsers
        public ActionResult GetChiefCurators(string role)
        {
            role = "30fae6a5-126f-4898-8440-fd666473659a";

            var user = context.Users.Where(p => p.Roles.Any(s=>s.RoleId.Equals(role))).ToList();

            return Json(new SelectList(user, "id", "email"));
        }

        public ActionResult GetCurators(string role)
        {
            role = "408e4cdb-b809-41ee-835c-69aac91273ab";

            var user = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).ToList();

            return Json(new SelectList(user, "id", "email"));
        }
    }
}