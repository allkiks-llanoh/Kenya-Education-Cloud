using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PublicationsDownloadSerializerNew
    {
        
        private readonly Publication _publication;

        public PublicationsDownloadSerializerNew(Publication publication)
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
        public String KICDNumber
        {
            get
            {
                return _publication.KICDNumber;
            }
        }
        public String Title
        {
            get
            {
                return _publication.Title;
            }
        }
      
    }
}
