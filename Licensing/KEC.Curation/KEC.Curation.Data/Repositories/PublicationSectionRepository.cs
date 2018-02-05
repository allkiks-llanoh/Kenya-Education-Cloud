using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class PublicationSectionRepository : Repository<PublicationSection>
    {
        public PublicationSectionRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
