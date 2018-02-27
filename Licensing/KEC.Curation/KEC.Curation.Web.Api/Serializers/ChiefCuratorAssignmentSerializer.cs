using KEC.Curation.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class  ChiefCuratorAssignmentSerializer
    {

        [Required(ErrorMessage = "Principal Curator Guid Is Requirred")]
        public string PrincipalCuratorGuid { get; set; }
        [Required(ErrorMessage = "Chief Curator Guid Is Requirred")]
        public string ChiefCuratorGuid { get; set; }
        [Required(ErrorMessage = "Publication KICD Number Is Required")]
        public string KICDNumber { get; set; }
        [Required(ErrorMessage = "Assignment Comments Are Required")]
        public string Notes { get; set; }
        [Required(ErrorMessage = "Publication stage cannot be null")]
        public PublicationStage Stage { get; set; }
        [Required(ErrorMessage = "Action taken cannot be blank")]
        public ActionTaken ActionTaken { get; set; }

    }
}
