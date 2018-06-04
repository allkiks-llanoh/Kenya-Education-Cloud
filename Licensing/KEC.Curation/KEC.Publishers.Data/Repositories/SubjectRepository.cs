using KEC.Publishers.Data.Database;
using KEC.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Publishers.Data.Repositories
{
    public class SubjectRepository : Repository<Subject>
    {
        public SubjectRepository(PublisherDataContext context) : base(context)
        {

        }
    }
}
