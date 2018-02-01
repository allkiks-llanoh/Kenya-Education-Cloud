using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class PublicationSectionRepository : Repository<PublicationSection>
    {
        public PublicationSectionRepository(CurationDataContext context) : base(context)
        {
        }
    }
}
