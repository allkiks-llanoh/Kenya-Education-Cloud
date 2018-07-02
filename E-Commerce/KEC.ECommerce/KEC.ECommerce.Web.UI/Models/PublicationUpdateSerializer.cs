using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KEC.ECommerce.Web.UI.Models
{
    public class PublicationUpdateSerializer: IPublicationSerializer
    {
        #region Publication
        [Required]
        public string ContentNumber { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public IFormFile ThumbnailImage { get; set; }
        [Required]
        public int Quantity { get; set; }
        #endregion


     

        #region Publisher
        [Required]
        public string PublisherGuid { get; set; }
        #endregion
    }
}
