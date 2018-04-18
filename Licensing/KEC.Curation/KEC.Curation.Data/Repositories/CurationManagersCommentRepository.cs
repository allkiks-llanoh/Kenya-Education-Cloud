using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Curation.Data.Repositories
{
    public class CurationManagersCommentRepository : Repository<CurationManagersComment>
    {
        public CurationManagersCommentRepository(CurationDataContext context) : base(context)
        {
        }
    }
}
