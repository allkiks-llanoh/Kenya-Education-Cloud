using System.ComponentModel.DataAnnotations;

namespace KEC.ECommerce.Web.UI.Security.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress, MaxLength(500)]
        [Display(Name = "Email Address:")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Login Password:")]
        public string Password { get; set; }

        [Display(Name = "Remember Me:")]
        public bool RememberMe { get; set; }
    }
}
