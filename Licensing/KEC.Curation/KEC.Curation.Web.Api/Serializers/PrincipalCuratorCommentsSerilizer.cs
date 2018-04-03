using KEC.Curation.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PrincipalCuratorCommentsSerilizer
    {
      
        [Required(ErrorMessage ="Publication Id Is Required")]
        public int PublicationId { get; set; }
        [Required(ErrorMessage = "You Have to provide Comments")]
        public string Notes { get; set; }
        [Required(ErrorMessage = "You Have to provide user Guid")]
        public string PrincipalCuratorGuid { get; set; }
        [Required(ErrorMessage = "Status is Required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Action Taken is a requirement")]
        public  ActionTaken ActionTaken { get; set; }
    }
}
