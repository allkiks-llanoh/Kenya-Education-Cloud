using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Publishers.Data.Models
{
    public class CuratorAssignment
    {
        public int Id { get; set; }
        public int PublicationSectionId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string Assignee { get; set; }
        public string AssignedBy { get; set; }
        public string Notes { get; set; }
        public bool Submitted { get; set; }
        public bool Status { get; set; }
        public int PublicationId { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual PublicationSection PublicationSection { get; set; }
    }
}
