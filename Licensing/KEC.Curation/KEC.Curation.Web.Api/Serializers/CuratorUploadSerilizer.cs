using System.ComponentModel.DataAnnotations;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CuratorUploadSerilizer
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "One Or More Curator Phone Number Filed Is Empty")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "One Or More Curator email Filed Is Empty")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "One Or More Curator Sir Name Filed Is Empty")]
        public string SirName { get; set; }
        [Required(ErrorMessage = "One Or More Curator First Name Filed Is Empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "One Or More Curator Last Name Filed Is Empty")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "One Or More Curator Type Filed Is Empty")]
        public string Type { get; set; }
    
    }
}
