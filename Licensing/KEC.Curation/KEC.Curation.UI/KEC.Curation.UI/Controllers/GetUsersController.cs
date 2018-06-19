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
        public ActionResult GetChiefCurators()
        {
            var role = "6c5175a7-372f-4cfe-a559-f862651813b2";

            var user = context.Users.Where(p => p.Roles.Any(s=>s.RoleId.Equals(role))).ToList();
         
            return Json(new SelectList(user, "FullName", "Id"));
        }
        public ActionResult GetChiefCuratorsList(string role)
        {
            role = "6c5175a7-372f-4cfe-a559-f862651813b2";

            var user = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).ToList();

            return Json(new SelectList(user, "FullName", "Email"));
        }

        public ActionResult GetCurators(int subjectId)
        {
            var role = "ff0a2466-90b3-468f-8ed5-60a170414131";

            var user = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))
                       && p.SubjectId.Equals(subjectId)).ToList();

            return Json(new SelectList(user, "FullName", "Id"));
        }
        public ActionResult GetCuratorsList(string role)
        {
            role = "ff0a2466-90b3-468f-8ed5-60a170414131";

            var user = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).ToList();

            return Json(new SelectList(user, "FullName", "Email"));
        }
        public ActionResult GetPrincipalCurators(string role)
        {
            role = "cac3eacd-c5b6-4c40-aaf3-72a48dfb5b2d";

            var user = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).ToList();

            return Json(new SelectList(user, "FullName", "Id"));
        }
        public ActionResult GetPrincipalCuratorsList(string role)
        {
            role = "cac3eacd-c5b6-4c40-aaf3-72a48dfb5b2d";

            var user = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).ToList();

            return Json(new SelectList(user, "FullName", "Email"));
        }
        public ActionResult GetAllUsers()
        {
            var _users = context.Users.ToList();

            return Json(new SelectList(_users, "FullName","Email"));
        }
        public int CountAllUsers()
        {
           
            var _users = context.Users.Count();
            return _users;
            
        }
        public int CountPrincipalCurators()
        {
            var role = "cac3eacd-c5b6-4c40-aaf3-72a48dfb5b2d";
            var _users = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).Count();
            return _users;

        }
        public int CountChiefCurators()
        {
            var role = "6c5175a7-372f-4cfe-a559-f862651813b2";
            var _users = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).Count();
            return _users;

        }
        public int CountCurators()
        {
            var role = "ff0a2466-90b3-468f-8ed5-60a170414131";
            var _users = context.Users.Where(p => p.Roles.Any(s => s.RoleId.Equals(role))).Count();
            return _users;

        }
        public ActionResult GetRoles()
        {
            var _users = context.Roles.ToList();

            return Json(new SelectList(_users, "Name", "Id"));
        }
    }
}