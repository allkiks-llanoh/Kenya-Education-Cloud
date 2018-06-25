using System.Linq;
using System.Threading.Tasks;
using KEC.ECommerce.Web.UI.Security.Models;
using KEC.ECommerce.Web.UI.Security.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace KEC.ECommerce.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
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
                SchoolCode = model.SchoolCode,
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
    }
}
