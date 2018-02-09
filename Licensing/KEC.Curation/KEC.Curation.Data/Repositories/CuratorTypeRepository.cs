using System;
using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class CuratorTypeRepository: Repository<CuratorType>
    {
        public CuratorTypeRepository (CurationDataContext context) : base(context)
        {
        }

      
      
    }
}
