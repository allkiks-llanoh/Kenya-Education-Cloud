using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Curation.Data.Repositories
{
   public class ChiefCuratorCommentRepository: Repository<ChiefCuratorComment>
    {
        public ChiefCuratorCommentRepository(CurationDataContext context) : base(context)
        {

        }
    }
}
