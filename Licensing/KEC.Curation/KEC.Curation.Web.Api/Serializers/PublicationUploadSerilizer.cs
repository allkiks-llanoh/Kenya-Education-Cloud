using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PublicationUploadSerilizer
    {
        [Required]
        public string Title { get; set; }
        public string ISBNNumber { get; set; }
        [Required]
        public string PublisherName { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required(ErrorMessage ="Publication completion Date cannot be blank")]
        [DisplayName("Completion Date")]
        public DateTime? CompletionDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage ="Please specified the publication subject")]
        [DisplayName("Subject")]
        public int? SubjectId { get; set; }
        [DisplayName("Level")]
        [Required(ErrorMessage = "Please specified the publication level")]
        public int? LevelId { get; set; }
        [Required(ErrorMessage = "Please upload the publication file")]
        public IFormFile PublicationFile { get; set; }
    }
}
