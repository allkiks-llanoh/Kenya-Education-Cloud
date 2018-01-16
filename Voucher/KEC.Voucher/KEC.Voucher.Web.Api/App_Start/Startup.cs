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
        private static string clientid = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string postlogoutredirecturi = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = clientid,
                Authority = authority,
                PostLogoutRedirectUri = postlogoutredirecturi,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthenticationFailed = context => {
                        context.HandleResponse();
                        context.Response.Redirect("/Error/messages=" + context.Exception.Message);
                        return Task.FromResult(0);
                    }
                }
            });
        }
       
    }
}  

