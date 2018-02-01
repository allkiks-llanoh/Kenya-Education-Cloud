using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Curation.Data.Models
{
    public class CuratorAssignment
    {
        public int Id { get; set; }
        public int PublicationSectionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Assignee { get; set; }
        public string AssignedBy { get; set; }
        public string Notes { get; set; }
        public virtual PublicationSection PublicationSection { get; set; }
    }
}
