using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        [Required]
        public DateTime CompletionDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage ="Please specified the publication subject")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Please specified the publication grade")]
        public int GradeId { get; set; }
        [Required(ErrorMessage = "Please upload the publication file")]
        public IFormFile FormFile { get; set; }
    }
}
