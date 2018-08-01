using System.ComponentModel.DataAnnotations;

namespace KEC.ECommerce.Web.UI.User.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
