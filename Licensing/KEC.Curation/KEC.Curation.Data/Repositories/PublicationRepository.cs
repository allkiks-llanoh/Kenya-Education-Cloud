using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KEC.Curation.Data.Repositories
{
    public class PublicationRepository : Repository<Publication>
    {
        public PublicationRepository(CurationDataContext context) : base(context)
        {
        }
    }
}
