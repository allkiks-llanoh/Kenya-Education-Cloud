using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class SubjectDownloadSerializerForCurators
    {
        private readonly Subject _subject;
        private readonly IUnitOfWork _uow;
        public SubjectDownloadSerializerForCurators(Subject subject, IUnitOfWork uow)
        {
            _uow = uow;
            _subject = subject;
        }
       
        public string Name
        {
            get
            {
                return _subject.Name;
            }
        }

        public int Id
        {
            get
            {
                return _subject.Id;
            }
        }
    }
}
