using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class SubjectRepository : Repository<Subject>
    {
        public SubjectRepository(IUnitOfWork context) : base(context)
        {
              
        }
    }
}
