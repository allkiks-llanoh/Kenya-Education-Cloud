
using KEC.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Publishers.Api.Serializers
{
    public class PublicationProcessingSerializer
    {
        [Required(ErrorMessage = "Publication KICDNumber cannot be blank")]
        public string KICDNumber { get; set; }
        public string Notes { get; set; }
        [Required(ErrorMessage = "Action taken cannot be blank")]
        public ActionTaken ActionTaken { get; set; }
        [Required(ErrorMessage = "User cannot be blank")]
        public string UserGuid { get; set; }
        [Required(ErrorMessage = "Publication stage cannot be null")]
        public PublicationStage Stage { get; set; }
    }
}
