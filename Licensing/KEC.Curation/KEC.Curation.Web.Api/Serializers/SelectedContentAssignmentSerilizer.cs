using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class SelectedContentAssignmentSerilizer
    {
        [Required(ErrorMessage = "Principal Curator Guid Is Requirred")]
        public string PrincipalCuratorGuid { get; set; }
        [Required(ErrorMessage = "Chief Curator Guid Is Requirred")]
        public string ChiefCuratorGuid { get; set; }
        public List<int> SelectedContent { get; set; }
    }
}
