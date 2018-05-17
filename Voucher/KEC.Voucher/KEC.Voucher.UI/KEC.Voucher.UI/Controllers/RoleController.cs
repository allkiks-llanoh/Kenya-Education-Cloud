using KEC.Voucher.UI.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KEC.Voucher.UI.Controllers
{
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
       
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
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
        public async Task<ActionResult> DeleteUserFromRoleChief(string guid)

        {
            var roleName = "30fae6a5-126f-4898-8440-fd666473659a";
            var role = await RoleManager.FindByIdAsync(roleName);
            var user = await UserManager.FindByEmailAsync(guid);
            await UserManager.RemoveFromRoleAsync(user.Id, role.Id);
            return RedirectToAction("/CurationManagers/ChiefCurators");
        }
        public ActionResult RemoveFromRole()
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
                var voucherUser = new VoucherUser
                {
                    Guid = user.Id,
               
                };
                return View(voucherUser);
            }
        }
    }
}
