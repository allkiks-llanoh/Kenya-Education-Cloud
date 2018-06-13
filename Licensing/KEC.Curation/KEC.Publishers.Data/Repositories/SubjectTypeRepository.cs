using KEC.Publishers.Data.Database;
using KEC.Publishers.Data.Models;

namespace KEC.Publishers.Data.Repositories
{
    public class SubjectTypeRepository : Repository<SubjectType>
    {
        public SubjectTypeRepository(PublisherDataContext context) : base(context)
        {
        }
    }
}
