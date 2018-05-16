using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Curation.UI.Models
{
    public class ToCurate
    {
        public int PublicationId { get; set; }
        public int AssignmentId { get; set; }
        public string Status { get; set; }
        public string Pending { get; set; }
        public string Publication { get; set; }
        public string SectionToCurate { get; set; }
        public string AssignmentDateUtc { get; set; }
    }
}