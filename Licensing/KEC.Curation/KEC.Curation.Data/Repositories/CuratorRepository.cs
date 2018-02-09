using System;
using System.Linq;
using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class CuratorRepository : Repository <CuratorCreation>
    {
        private readonly CurationDataContext _curationDBContext;
        public CuratorRepository(CurationDataContext context) : base(context)
        {
            _curationDBContext = context as CurationDataContext;
        }

        public void AddFromCSV(CuratorCreation curatorCreation)
        {
            var retrievedCurator = _curationDBContext.CuratorCreations
                                 .FirstOrDefault(p => p.FirstName.Equals(curatorCreation.FirstName)
                                 && p.LastName.Equals(curatorCreation.LastName));
            if (retrievedCurator ==null)
            {
                Add(curatorCreation);
            }
              
        }

        
    }
}
