using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using KEC.Curation.UI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using KEC.Curation.UI.ActionFilters;
using Newtonsoft.Json.Linq;

namespace KEC.Curation.UI.Controllers
{
    [Authorize]
    [AllowCrossSiteJson]
    [UserGuidJson]
    public class UserProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private string appKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private string graphResourceID = "https://graph.microsoft.com";
        private string graphResourceIDGroups = "https://graph.microsoft.com/v1.0/groups/042044dc-a1f4-41c5-9927-02eeece02394/members";

        // GET: UserProfile
        public async Task<ActionResult> Index()
        {
            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            try
            {
                Uri servicePointUri = new Uri(graphResourceID);
                Uri serviceRoot = new Uri(servicePointUri, tenantID);
                ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                      async () => await GetTokenForApplication());

                // use the token for querying the graph to get the user details

                var result = await activeDirectoryClient.Users
                    .Where(u => u.ObjectId.Equals(userObjectID))
                    .ExecuteAsync();
                IUser user = result.CurrentPage.ToList().First();

                return View(user);
            }
            catch (AdalException)
            {
                // Return to error page.
                return View("Error");
            }
            // if the above failed, the user needs to explicitly re-authenticate for the app to obtain the required token
            catch (Exception)
            {
                return View("Relogin");
            }
        }
        public async Task<ActionResult> GetChiefCurators()
        {

            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            Uri servicePointUri = new Uri(graphResourceID);
            Uri serviceRoot = new Uri(servicePointUri, tenantID);
            var curatorsList = new List<User>();
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                                                         async () => await GetTokenForApplication());

            IPagedCollection<IUser> pagedCollection = await activeDirectoryClient.Users.OrderBy
                                                      (u => u.DisplayName).ExecuteAsync();
            if (pagedCollection != null)
            {
                do
                {
                    List<IUser> curatorList = pagedCollection.CurrentPage.ToList();
                    foreach (IUser user in curatorList)
                    {
                        curatorList.Add((User)user);
                    }
                    pagedCollection = await pagedCollection.GetNextPageAsync();

                }
                while (pagedCollection != null);
            }
            return View(curatorsList);
        }
        public void RefreshSession()
        {
            HttpContext.GetOwinContext().Authentication.Challenge(
                new AuthenticationProperties { RedirectUri = "/UserProfile" },
                OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }
        [HttpGet]

        public async Task<string> GetTokenForApplication()
        {
           
            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            // get a token for the Graph without triggering any user interaction (from the cache, via multi-resource refresh token, etc)
            ClientCredential clientcred = new ClientCredential(clientId, appKey);
            // initialize AuthenticationContext with the token cache of the currently signed in user, as kept in the app's database
            AuthenticationContext authenticationContext = new AuthenticationContext(aadInstance + tenantID, new ADALTokenCache(signedInUserID));
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenSilentAsync(graphResourceID, clientcred, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get, graphResourceIDGroups);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            var response = await client.SendAsync(request);
            var result = response.Content.ReadAsStringAsync().Result;
            var Rarray = JObject.Parse(result);
            return (result);
        }
       
    }
}