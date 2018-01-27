using System;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using KEC.Voucher.Data.UnitOfWork;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(KEC.Voucher.Web.Api.App_Start.StartUp))]
namespace KEC.Voucher.Web.Api.App_Start
{
    public class StartUp
    {
       
     
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
           
        }
       
    }
}  

