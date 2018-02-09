using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class GetCuratorsRepository: Repository<GetCuratorsRepository>
    {
        public GetCuratorsRepository(CurationDataContext context) : base(context)
        {
        }
    }
}
