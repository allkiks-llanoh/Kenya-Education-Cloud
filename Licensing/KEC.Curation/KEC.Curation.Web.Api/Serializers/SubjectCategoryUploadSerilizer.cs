using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class SubjectCategoryUploadSerilizer
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Category Name cannot be blank")]
        public string Name { get; set; }
    }
}
