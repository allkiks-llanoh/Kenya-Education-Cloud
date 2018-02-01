﻿using System.Collections.Generic;

namespace KEC.Curation.Data.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
    }
}