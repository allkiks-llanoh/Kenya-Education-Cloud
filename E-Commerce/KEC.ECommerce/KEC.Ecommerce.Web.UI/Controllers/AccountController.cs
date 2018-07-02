﻿using System.Linq;
using System.Threading.Tasks;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using KEC.ECommerce.Web.UI.Pagination;
using KEC.ECommerce.Web.UI.Security.Models;
using KEC.ECommerce.Web.UI.Security.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace KEC.ECommerce.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _uow;
        private readonly IPageHelper<Licence> _paginationHelper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , IPageHelper<Licence> paginationHelper, IUnitOfWork uow)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _uow = uow;
            _paginationHelper = paginationHelper;
            _paginationHelper.PageConfig.PageSize = 10;
        }
        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login failed,invalid Email address or Password ");
                return View();
            }
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return RedirectToUrl("Dashboard", "Account", returnUrl);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToUrl("Index", "Home", returnUrl);
            }
        }
        private IActionResult RedirectToUrl(string fallbackAction, string fallbackController, string redirectUrl)
        {
            if (Url.IsLocalUrl(redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            else
            {
                return RedirectToAction(fallbackAction, fallbackController);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser
            {
                Email = model.EmailAddress,
                UserName = model.EmailAddress,
                IdentificationCode = model.IdentificationCode,
                LastName = model.LastName,
                FirstName = model.FirstName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View();
            }
            return RedirectToAction("Login");
        }
        [Authorize]
        public IActionResult Orders()
        {
            var mail = User.FindFirst("Email")?.Value;
            var orders = _uow.OrdersRepository.Find(p => p.CustomerEmail.Equals(mail) && p.Status == OrderStatus.Submitted);
            var model = orders?.Select(p => new OrderViewModel(_uow, p))?.OrderByDescending(p=> p.OrderDate)?.ToList();
            return View(model);
        }
        [Authorize]
        public IActionResult Licences(int pageNumber = 1, string searchTerm=null)
        {
            var code = User.FindFirst("IdentificationCode")?.Value;
            var licencesQuery = _uow.LicencesRepository.QueryableLicences(code, searchTerm);
            var result = _paginationHelper.GetPage(licencesQuery, pageNumber);
            var model = new LicencePageViewModel
            {
                Licences = result.Items?.Select(p => new LicenceViewModel(_uow, p)),
                Pager = result.Pager
            };
            if (searchTerm != null)
            {
                return PartialView("_LicenceListPartial", model);
            }
            else
            {
                return View(model);
            }
        }
    }
}
