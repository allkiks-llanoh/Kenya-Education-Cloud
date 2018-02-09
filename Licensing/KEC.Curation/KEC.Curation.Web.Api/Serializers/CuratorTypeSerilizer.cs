using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KEC.Curation.Data.Repositories;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CuratorTypeSerilizer
    {
       
      public int? Id { get; set; }
        [Required(ErrorMessage = "Type Name cannot be blank")]
        public string TypeName{ get; set; }
    }
}
