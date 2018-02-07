using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Curation.Data.Models
{
    public class Publication
    {
        public Publication()
        {
            PublicationStageLogs = new List<PublicationStageLog>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string KICDNumber { get; set; }
        public string ISBNNumber { get; set; }
        public string PublisherName { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public int SubjectId { get; set; }
        public int LevelId { get; set; }
        public DateTime CreatedTimeUtc { get; set; }
        public DateTime ModifiedTimeUtc { get; set; }
        public string CertificateNumber { get; set; }
        public string CertificateUrl { get; set; }
        public string Owner { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<PublicationStageLog> PublicationStageLogs { get; set; }
    }
}
