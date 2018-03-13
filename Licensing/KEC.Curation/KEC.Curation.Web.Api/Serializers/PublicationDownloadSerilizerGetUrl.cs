using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Linq;
using KEC.Curation.Services.Extensions;
using System.ComponentModel.DataAnnotations;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PublicationDownloadSerilizerGetUrl
    {
        private readonly Publication _publication;
        private readonly IUnitOfWork _uow;

        public PublicationDownloadSerilizerGetUrl(Publication publication, IUnitOfWork uow)
        {
            _publication = publication;
            _uow = uow;

        }
        
       
        public string Url
        {
            get
            {
                return _publication.Url;
            }
        }
        
    }
}
