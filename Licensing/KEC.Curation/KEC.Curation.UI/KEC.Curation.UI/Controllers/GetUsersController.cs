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
            role = "6b1e1d44-d286-45c9-9ee8-295c6b240086";

            var user = context.Users.Where(p => p.Roles.Any(s=>s.RoleId.Equals(role))).ToList();

            return Json(new SelectList(user, "id", "email"));
        }

        public ActionResult GetCurators(string role)
        {
            role = "f8dcc78a-4303-472e-9d2d-e59c1eb8bc1d";

            var user = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).ToList();

            return Json(new SelectList(user, "id", "email"));
        }
    }
}