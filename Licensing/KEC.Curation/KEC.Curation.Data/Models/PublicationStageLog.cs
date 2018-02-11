using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Curation.Data.Models
{
     public class PublicationStageLog
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public int PublicationId { get; set; }
        public PublicationStage Stage { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public ActionTaken? ActionTaken { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
