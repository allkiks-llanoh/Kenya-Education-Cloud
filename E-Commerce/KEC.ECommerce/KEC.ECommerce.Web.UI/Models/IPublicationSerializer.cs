using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public interface IPublicationSerializer
    {
        string ContentNumber { get; set; }
        string PublisherGuid { get; set; }
        IFormFile ThumbnailImage { get; set; }
    }
}
