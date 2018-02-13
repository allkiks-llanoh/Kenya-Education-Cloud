using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PublicationUploadSerilizer
    {
        [Required(ErrorMessage = "Publication title cannot be blank")]
        public string Title { get; set; }
        public string ISBNNumber { get; set; }
        [Required(ErrorMessage = "Publication publisher cannot be blank")]
        public string PublisherName { get; set; }
        [Required(ErrorMessage = "Publication author cannot be blank")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Publication price cannot be blank")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Publication completion Date cannot be blank")]
        [DisplayName("Completion Date")]
        public DateTime? CompletionDate { get; set; }
        [Required(ErrorMessage = "Please provide brief descriptio (200 words)")]
        [StringLength(200,ErrorMessage ="A maximum of 2oo words is required")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Please specified the publication subject")]
        [DisplayName("Subject")]
        public int SubjectId { get; set; }
        [DisplayName("Level")]
        [Required(ErrorMessage = "Please specified the publication level")]
        public int LevelId { get; set; }
        [Required(ErrorMessage = "Please upload the publication file")]
        public IFormFile PublicationFile { get; set; }
        [Required]
        public string UserGuid { get; set; }
    }
}
