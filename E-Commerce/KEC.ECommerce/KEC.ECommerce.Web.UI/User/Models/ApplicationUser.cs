using Microsoft.AspNetCore.Identity;

namespace KEC.ECommerce.Web.UI.User.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string IdentificationCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
