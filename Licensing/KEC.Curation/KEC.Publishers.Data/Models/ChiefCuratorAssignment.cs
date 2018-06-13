using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Publishers.Data.Models
{
    public class ChiefCuratorAssignment
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public string PrincipalCuratorGuid { get; set; }
        public string ChiefCuratorGuid { get; set; }
        public bool Submitted { get; set; }
        public bool Assigned { get; set; }
        public DateTime AssignmetDateUtc { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual PublicationSection PublicationSection { get; set; }
    }
}
