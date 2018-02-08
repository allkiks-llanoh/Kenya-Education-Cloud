﻿using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Models
{
    public class SubjectType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}