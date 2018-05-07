using Authentication_Test.Models;
using KEC.Curation.UI.Models;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

            return Json(user);
        }

        public ActionResult GetCurators(string role)
        {
            role = "408e4cdb-b809-41ee-835c-69aac91273ab";

            var user = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).ToList();

            return Json(user);
        }
        public ActionResult GetPrincipalCurators(string role)
        {
            role = "7565571d-07c4-4bfe-9587-9aef63adb321";

            var user = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).ToList();

            return Json(user);
        }
        public ActionResult GetAllUsers()
        {
            var _users = context.Users.ToList();

            return Json(_users);
        }
        public int CountAllUsers()
        {
           
            var _users = context.Users.Count();
            return _users;
            
        }
        public int CountPrincipalCurators()
        {
            var role = "7565571d-07c4-4bfe-9587-9aef63adb321";
            var _users = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).Count();
            return _users;

        }
        public int CountChiefCurators()
        {
            var role = "30fae6a5-126f-4898-8440-fd666473659a";
            var _users = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).Count();
            return _users;

        }
        public int CountCurators()
        {
            var role = "408e4cdb-b809-41ee-835c-69aac91273ab";
            var _users = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).Count();
            return _users;

        }
    }
}