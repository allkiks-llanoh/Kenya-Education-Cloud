using System.ComponentModel.DataAnnotations;


namespace KEC.Curation.Web.Api.Serializers
{
    public class LevelsUploadSerilizer
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Grade Level Cannot Be Empty")]
        public string Name { get; set; }
    }
}
