using System.ComponentModel.DataAnnotations;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CurationUploadSerializer
    {
     
        [Required(ErrorMessage ="Notes cannot be blank")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        public bool Submitted { get; set; }
        [Required(ErrorMessage = "User cannot be blank")]
        public string UserGuid { get; set; }
    }
}
