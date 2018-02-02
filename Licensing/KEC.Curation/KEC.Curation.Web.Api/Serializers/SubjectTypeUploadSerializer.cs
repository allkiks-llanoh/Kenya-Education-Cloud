using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class SubjectTypeUploadSerializer
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Subject type name cannot be blank")]
        public string Name { get; set; }
    }
}
