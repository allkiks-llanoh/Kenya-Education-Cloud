using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class FinanceDownloadSerilizer
    {
        private readonly Publication _publication;
        private readonly IUnitOfWork _uow;
        public FinanceDownloadSerilizer(Publication publication, IUnitOfWork uow)
        {
            _uow = uow;
            _publication = publication;
     
        }
        public int Id
        {
            get
            {
                return _publication.Id;
            }
        }
        public string Title
        {
            get
            {
                return _publication.Title;
            }
        }
        public string KICDNumber
        {
            get
            {
                return _publication.KICDNumber;
            }
        }
    }
}
