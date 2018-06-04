using KEC.Publishers.Data.Database;
using KEC.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Publishers.Data.Repositories
{
    public class PublicationStageLogRepository : Repository<PublicationStageLog>
    {
        public PublicationStageLogRepository(PublisherDataContext context) : base(context)
        {
        }
    }
}
