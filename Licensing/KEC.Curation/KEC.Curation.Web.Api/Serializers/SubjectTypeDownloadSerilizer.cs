using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class SubjectTypeDownloadSerilizer
    {
        private readonly SubjectType _subjectType;
        private readonly IUnitOfWork _uow;
        public SubjectTypeDownloadSerilizer(SubjectType subjectType, IUnitOfWork uow)
        {
            _uow = uow;
            _subjectType = subjectType;
        }

        public string Name
        {
            get
            {
                return _subjectType.Name;
            }
        }

        public int Id
        {
            get
            {
                return _subjectType.Id;
            }
        }
    }
}
