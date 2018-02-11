using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class LevelRepository : Repository<Level>
    {
        public LevelRepository(CurationDataContext context) : base(context)
        {
        }
    }
}
