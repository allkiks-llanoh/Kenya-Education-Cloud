﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class SubjectUploadSerializer
    {
        [Required(ErrorMessage ="Subject name cannot be blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Subject type cannot be blank")]
        [DisplayName("Subject Type")]
        public int? SubjectTypeId { get; set; }
    }
}
