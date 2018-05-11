using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class DeleteSerilizer
    {
        [Required(ErrorMessage = "Id Is Required")]
        public int? Id { get; set; }
    }
}
