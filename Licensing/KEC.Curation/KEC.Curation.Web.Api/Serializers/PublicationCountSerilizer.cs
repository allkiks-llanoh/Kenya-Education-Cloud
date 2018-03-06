using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PublicationCountSerilizer
    {
        
        [Required]
        public string UserGuid { get; set; }
        [Required]
        public string Stage { get; set; }
    }
}
