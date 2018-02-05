using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class PublicationStageRepository : Repository<PublicationStage>
    {
        public PublicationStageRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
