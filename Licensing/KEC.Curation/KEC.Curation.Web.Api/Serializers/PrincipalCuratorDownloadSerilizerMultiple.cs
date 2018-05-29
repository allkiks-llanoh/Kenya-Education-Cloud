using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PrincipalCuratorDownloadSerilizerMultiple
    {
        private readonly ChiefCuratorAssignment _publication;

        public PrincipalCuratorDownloadSerilizerMultiple(ChiefCuratorAssignment publication)
        {
            _publication = publication;

        }
        public int Id
        {
            get
            {
                return _publication.Id;
            }
        }

        
       
    }
}
