using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class SubjectTypeRepository : Repository<SubjectType>
    {
        public SubjectTypeRepository(CurationDataContext context) : base(context)
        {
        }
    }
}
