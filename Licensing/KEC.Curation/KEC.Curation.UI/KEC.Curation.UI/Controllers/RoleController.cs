﻿
using KEC.Curation.UI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KEC.Curation.UI.Controllers
{
    //[CustomAuthorize(Roles = "Admin")]
    [AllowAnonymous]
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        public RoleController()
        {
        }
        
        public RoleController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            RoleManager = roleManager;
            UserManager = UserManager;

        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Role
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles)
                list.Add(new RoleViewModel(role));
            return View(list);
        }
        public async Task<string> Subjects(RegisterViewModel model)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri("https://curationapi-d.kec.ac.ke/api/SubjectTypes");

                var response = await client.GetAsync(uri);

                string textResult = await response.Content.ReadAsStringAsync();
              

                return textResult;
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <ActionResult> Create(RoleViewModel model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            return View(new RoleViewModel(role));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        public async Task<ActionResult> DeleteConfirmed(string id)

        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRole(RoleRemoveViewModel model)

        {
            var user = await UserManager.FindByNameAsync(model.UserName);
          
            var roleInUse =await  UserManager.GetRolesAsync(user.Id);

            var result = await UserManager.RemoveFromRolesAsync(user.Id, roleInUse.ToArray());

            return RedirectToAction("ChiefCurators", "CurationManagers");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToRole(RoleRemoveViewModel model)

        {
            var user = await UserManager.FindByNameAsync(model.UserName);

            var roleToAsign = await RoleManager.FindByNameAsync(model.RoleName);

            var result = await UserManager.AddToRoleAsync(user.Id, model.RoleName);

            return RedirectToAction("Index", "CurationManagers");

        }
        public ActionResult RemoveFromRole()
        {
                return View();   
        }
        public ActionResult UserManagement()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "";
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.Roles = list;
            return View();           
            
        }
        public ActionResult ListUsers()
        {
            ViewData["SubTitle"] = "Curation Management System";
            ViewData["Message"] = "";
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var chiefCurator = new ChiefCurators
                {
                    Guid = user.Id,
                    Subjectid = user.SubjectId,
                    FullName = user.FullName
                };
                return View(chiefCurator);
            }
        }
    }
}