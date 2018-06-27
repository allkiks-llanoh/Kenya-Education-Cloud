using KEC.ECommerce.Web.UI.Security.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KEC.ECommerce.Web.UI.Security.Database
{
    public class IdentityDataContext : IdentityDbContext<ApplicationUser> 
    {

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options)
        {
       
        }
    }
}
