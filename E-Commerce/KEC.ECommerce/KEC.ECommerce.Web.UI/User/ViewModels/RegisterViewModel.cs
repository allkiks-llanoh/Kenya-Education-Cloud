using System.ComponentModel.DataAnnotations;

namespace KEC.ECommerce.Web.UI.User.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress,MaxLength(500)]
        [Display(Name = "Email Address:")]
        public string EmailAddress { get; set; }
        [Required] 
        [DataType(DataType.Password),MinLength(8)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage ="Passwords must match")]
        [Display(Name ="Confirm Password:")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Identification Code:")]
        [MaxLength(100)]
        public string IdentificationCode { get; set; }
        [Required]
        [Display(Name ="First Name:"),MaxLength(200)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name:"), MaxLength(200),Required]
        public string LastName { get; set; }
    }
}
