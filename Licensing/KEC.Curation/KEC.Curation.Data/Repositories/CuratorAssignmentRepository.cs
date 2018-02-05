using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class CuratorAssignmentRepository : Repository<CuratorAssignment>
    {
        public CuratorAssignmentRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
