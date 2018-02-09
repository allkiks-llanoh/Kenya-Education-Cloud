using KEC.Curation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class SubjectCategoryDownloadSerilizer
    {
        private readonly SubjectCategory _subjectCategory;

        public SubjectCategoryDownloadSerilizer (SubjectCategory subjectCategory)
        {
            _subjectCategory = subjectCategory;
        }
        public int Id
        {
            get
            {
                return _subjectCategory.Id;
            }
        }
        public string Name
        {
            get
            {
                return _subjectCategory.Name;
            }
        }
    }
}
