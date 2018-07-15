using KEC.ECommerce.Web.UI.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Security.Extensions
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("DisplayName", user.DisplayName ?? string.Empty));
            identity.AddClaim(new Claim("Email", user.Email ?? string.Empty));
            identity.AddClaim(new Claim("IdentificationCode", user.IdentificationCode ?? string.Empty));
            return identity;
        }
    }
}
