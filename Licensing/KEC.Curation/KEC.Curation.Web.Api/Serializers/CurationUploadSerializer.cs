using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CurationUploadSerializer
    {
        [Required(ErrorMessage ="Notes cannot be blank")]
        public string Notes { get; set; }
        public bool Submitted { get; set; }
    }
}
