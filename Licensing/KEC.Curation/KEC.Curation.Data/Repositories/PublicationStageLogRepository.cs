﻿using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class PublicationStageLogRepository : Repository<PublicationStageLog>
    {
        public PublicationStageLogRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
