using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KEC.ECommerce.Web.UI.Models
{
    public class PublicationUploadSerializer: IPublicationSerializer
    {
        #region Publication
        [Required]
        public string ContentNumber { get; set; }
        [Required]
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public IFormFile ThumbnailImage { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [MaxLength(2048)]
        public string ContentUrl { get; set; }
        #endregion


        #region Author
        [Required]
        public string AuthorFirstName { get; set; }
        [Required]
        public string AuthorLastName { get; set; }
        #endregion

        #region Category
        [Required]
        public string Category { get; set; }
        #endregion
        #region Subject
        [Required]
        public string Subject { get; set; }
        #endregion
        #region Level
        [Required]
        public string Level { get; set; }
        #endregion

        #region Publisher
        [Required]
        public string PublisherName { get; set; }
        [Required]
        public string PublisherGuid { get; set; } 
        #endregion

    }
}
