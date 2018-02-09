using System;
using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;

namespace KEC.Curation.Data.Repositories
{
    public class SubjectCategoryRepository: Repository<SubjectCategory>
    {
        public SubjectCategoryRepository (CurationDataContext context) : base(context)
        {

        }
    }
}
