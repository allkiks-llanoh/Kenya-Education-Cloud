using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        // GET api/<controller>
        [HttpGet,Route("login")]
        public void LogIn()
        {
            if (!User.Identity.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
                {
                    RedirectUri = "/"
                },OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        // DELETE api/<controller>/5
        [HttpGet,Route("logout")]
        public void SignOut()
        {
            HttpContext.Current.GetOwinContext().Authentication.
                SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, 
                CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}