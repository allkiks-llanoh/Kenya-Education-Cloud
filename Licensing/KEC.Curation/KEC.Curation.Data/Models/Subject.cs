using System;
using System.Collections.Generic;

namespace KEC.Curation.Data.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public int SubjectTypeId { get; set; }
        public string Name { get; set; }
        public  SubjectType SubjectType { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
    }
}