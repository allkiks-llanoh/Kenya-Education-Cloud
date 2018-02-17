using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CurationContentAssignmentSerializer
    {
        [DefaultValue("Whole content")]
        public string Section { get; set; }
        [Required(ErrorMessage ="Assigned by cannot be blank")]
        public string AssignedBy { get; set; }
        [Required(ErrorMessage = "Assignee by cannot be blank")]
        public string Assignee { get; set; }
        public bool FullyAssigned { get; set; }
        
    }
}
