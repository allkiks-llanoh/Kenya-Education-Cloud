using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class ChiefFlagSubmittedSerilizer
    {
        [Required(ErrorMessage = "Submitted cannot be blank")]
        public bool Submitted { get; set; }
        [Required(ErrorMessage = "User cannot be blank")]
        public string UserGuid { get; set; }
    }
}
