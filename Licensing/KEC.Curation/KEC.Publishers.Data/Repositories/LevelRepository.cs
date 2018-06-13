using KEC.Publishers.Data.Database;
using KEC.Publishers.Data.Models;

namespace KEC.Publishers.Data.Repositories
{
    public class LevelRepository : Repository<Level>
    {
        public LevelRepository(PublisherDataContext context) : base(context)
        {
        }
    }
}
