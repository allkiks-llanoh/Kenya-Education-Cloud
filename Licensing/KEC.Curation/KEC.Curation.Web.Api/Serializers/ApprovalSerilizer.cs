using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class ApprovalSerilizer
    {
        public int? Id { get; set; }
      
        [Required(ErrorMessage = "Cannot Submit Without Comments")]
        public string Notes { get; set; }
    }
}
