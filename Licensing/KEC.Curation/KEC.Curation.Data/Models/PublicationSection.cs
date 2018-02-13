using System;

namespace KEC.Curation.Data.Models
{
    public class PublicationSection
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public string SectionDescription { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual CuratorAssignment CuratorAssignment { get; set; }
    }
}
