using KEC.Curation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Publishers.Api.Serializers
{
    public class SubjectTypeDownloadSerializer
    {
        private readonly SubjectType _subjectType;

        public SubjectTypeDownloadSerializer(SubjectType subjectType)
        {
            _subjectType = subjectType;
        }
        public int Id
        {
            get
            {
                return _subjectType.Id;
            }
        }
        public string Name
        {
            get
            {
                return _subjectType.Name;
            }
        }
    }
}
