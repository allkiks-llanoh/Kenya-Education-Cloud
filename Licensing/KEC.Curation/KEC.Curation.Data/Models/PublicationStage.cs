using System.Collections.Generic;

namespace KEC.Curation.Data.Models
{
    public class PublicationStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public virtual ICollection<PublicationStageLog> PublicationStageLogs { get; set; }
    }
}
