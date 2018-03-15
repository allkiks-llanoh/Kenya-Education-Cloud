using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PrincipalCuratorReadCommentsUploadSerilizer
    {

        [Required(ErrorMessage = "Publication Id Is Required")]
        public int PublicationId { get; set; }
    }
}
