using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Curation.Data.Models
{
   public class PrincipalCuratorComment
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public string Notes { get; set; }
        public string PrincipalCuratorGuid { get; set; }
        public bool Submitted { get; set; }
    }
}
