using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Hangfire;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Helpers;
using KEC.ECommerce.Web.UI.Mailer;
using KEC.ECommerce.Web.UI.Models;
using KEC.ECommerce.Web.UI.Pagination;
using KEC.ECommerce.Web.UI.Security.Models;
using KEC.ECommerce.Web.UI.Security.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace KEC.ECommerce.Web.UI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _uow;
        private readonly IPageHelper<Order> _orderPaginationHelper;
        private readonly IPageHelper<Licence> _licencePaginationHelper;
        private readonly IHostingEnvironment _env;
        private readonly IEmailService _emailService;
        private readonly IEmailConfiguration _emailConfiguration;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , IPageHelper<Licence> licencePaginationHelper, IPageHelper<Order> orderPaginationHelper, IUnitOfWork uow,
            IEmailService emailService, IHostingEnvironment env, IEmailConfiguration emailConfiguration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _uow = uow;
            _orderPaginationHelper = orderPaginationHelper;
            _licencePaginationHelper = licencePaginationHelper;
            _licencePaginationHelper.PageConfig.PageSize = 10;
            _orderPaginationHelper.PageConfig.PageSize = 10;
            _env = env;
            _emailService = emailService;
            _emailConfiguration = emailConfiguration;
        }

        [BreadCrumb(Title = "Dashboard", Order = 1)]
        public IActionResult Dashboard()
        {
            return View();
        }
        [AllowAnonymous]
        [BreadCrumb(Title = "Register", Order = 1)]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [AllowAnonymous]
        [BreadCrumb(Title = "Login", Order = 1)]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [BreadCrumb(Title = "Pending Orders", Order = 1)]
        public IActionResult Orders()
        {
            var mail = User.FindFirst("Email")?.Value;
            var orders = _uow.OrdersRepository.Find(p => p.CustomerEmail.Equals(mail) && p.Status == OrderStatus.Submitted);
            var model = orders?.Select(p => new OrderViewModel(_uow, p))?.OrderByDescending(p => p.OrderDate)?.ToList();
            return View(model);
        }
        [BreadCrumb(Title = "Licences", Order = 1)]
        public IActionResult Licences(int pageNumber = 1, string searchTerm = null)
        {
            var code = User.FindFirst("IdentificationCode")?.Value;
            var licencesQuery = _uow.LicencesRepository.QueryableLicences(code, searchTerm);
            var result = _licencePaginationHelper.GetPage(licencesQuery, pageNumber);
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
        [HttpGet]
        [BreadCrumb(Title = "Purchases", Order = 1)]
        public IActionResult Purchases(int pageNumber = 1, string searchTerm = null)
        {
            var mail = User.FindFirst("Email")?.Value;
            var ordersQuery = _uow.OrdersRepository.QueryableOrders(mail, searchTerm);
            var result = _orderPaginationHelper.GetPage(ordersQuery, pageNumber);
            var model = new PurchasePageViewModel
            {
                Orders = result.Items?.Select(p => new OrderViewModel(_uow, p)),
                Pager = result.Pager
            };
            if (searchTerm != null)
            {
                return PartialView("_PurchasesListPartial", model);
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Purchase(int orderId)
        {
            var mail = User.FindFirst("Email")?.Value;
            var order = await _uow.OrdersRepository.GetOrderByUser(orderId, mail, OrderStatus.Paid);
            if (order == null)
            {
                return NotFound();
            }
            return View(new OrderViewModel(_uow, order, true));
        }
        [HttpGet]
        [AllowAnonymous]
        [BreadCrumb(Title = "Forgot Password", Order = 1)]
        public IActionResult ForgotPassword(string email)
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        [BreadCrumb(Title = "Forgot Password Confirmation", Order = 1)]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.EmailAddress);
                if (user != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var hostname = Request.Host.Host;
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { code }, protocol: HttpContext.Request.Scheme);
                    var email = $"&EmailAddress={Uri.EscapeDataString(model.EmailAddress)}";
                    callbackUrl += email;
                    var callbackLink = $"<a class='btn' href='{callbackUrl}'>Reset my password »</a>";
                    var pathToTemplate = Path.Combine(_env.ContentRootPath, "Mailer", "Templates", "PasswordReset.html");
                    var builder = new BodyBuilder();

                    using (var SourceReader = System.IO.File.OpenText(pathToTemplate))
                    {

                        builder.HtmlBody = SourceReader.ReadToEnd();
                       
                    }
                    string messageBody = builder.HtmlBody.Replace("@Name", user.DisplayName)
                                                .Replace("@CallbackLink", callbackLink)
                                                .Replace("@Host",hostname)
                                                .Replace("@CallbackUrl",callbackUrl);
                    var emailMessage = new EmailMessage
                    {
                        Content = messageBody
                    };
                    emailMessage.ToAddresses.Add(new EmailAddress { Name = user.DisplayName, Address = user.Email });
                    emailMessage.FromAddresses.Add(new EmailAddress { Name = _emailConfiguration.SmtpUsername,
                                                    Address = _emailConfiguration.SmtpUsername });
                    emailMessage.Subject = "Password Reset";
                    var emailService = _emailService as EmailService;
                    var emailConfiguration = _emailConfiguration as EmailConfiguration;
                    BackgroundJob.Enqueue(() => MailerActions.SendEmail(emailMessage, emailService, emailConfiguration));
                }
                return RedirectToAction(nameof(AccountController.ForgotPasswordConfirmation),"Account");
            }
            else
            {
                return View(model);
            }
        }
        
       

        [HttpGet]
        [AllowAnonymous]
        [BreadCrumb(Title = "Reset Password", Order = 1)]
        public IActionResult ResetPassword(string code = null, string email = null)
        {
            if (code == null)
            {
                return NotFound();
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            AddErrors(result);
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        [BreadCrumb(Title = "Reset Password Confirmation", Order = 1)]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        [BreadCrumb(Title = "Profile", Order = 1)]
        public IActionResult Profile()
        {
            var code = User.FindFirst("IdentificationCode")?.Value;
            var email = User.FindFirst("Email")?.Value;
            var fullName = User.FindFirst("DisplayName")?.Value;
            var model = new ProfileViewModel(code, fullName, email);
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
