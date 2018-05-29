using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Controllers
{
    public class CurationManagersGetCommentsSerilizer
    {
        private readonly CurationManagersComment _assignment;
        private readonly IUnitOfWork _uow;
        public CurationManagersGetCommentsSerilizer(CurationManagersComment assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }
        public string Publication
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return publication.KICDNumber;
            }
        }
        public string Title
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return publication.KICDNumber;
            }
        }
        public int publicationId
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return publication.Id;
            }
        }

        public string PublicationUrl
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                var publicationId = _uow.PublicationSectionRepository.Find(p => p.PublicationId.Equals(publication.Id)).FirstOrDefault();
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    return publication.Url;
                }
            }
        }
    }
}
