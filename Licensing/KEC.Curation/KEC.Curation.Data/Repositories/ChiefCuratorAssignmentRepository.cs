using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class ChiefCuratorAssignmentRepository : Repository<ChiefCuratorAssignment>
    {
        public ChiefCuratorAssignmentRepository(CurationDataContext context) : base(context)
        {
        }
    }
}
