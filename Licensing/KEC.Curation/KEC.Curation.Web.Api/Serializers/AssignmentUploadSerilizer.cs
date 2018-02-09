using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class AssignmentUploadSerilizer
    {
        public int? Id { get; set; }
        public int PublicationSectionId { get; set; }
        [Required(ErrorMessage = "You Must Select A Curator To Assign To")]
        public int Assignee { get; set; }
        public int AssignedBy { get; set; }
        [Required(ErrorMessage = "Cannot Submit Without Comments")]
        public string Notes { get; set; }
      
    }
}
