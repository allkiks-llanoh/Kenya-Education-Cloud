using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CurationCommentSerializer
    {
        private readonly ChiefCuratorAssignment _assignment;

        private readonly IUnitOfWork _uow;

        public CurationCommentSerializer(ChiefCuratorAssignment assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }
       
        public int PublicationId
        {
            get
            {
                return _assignment.Publication.Id;
            }
        }
       
       
        public string Status
        {
            get
            {
                return _assignment.Submitted ? "Submitted" : "Pending";
            }
        }
        public string Notes
        {
            get
            {
                var publicationId = _uow.PublicationSectionRepository.Get(_assignment.Publication.Id)?.PublicationId;
                var publication = _uow.ChiefCuratorCommentRepository.Get(publicationId.GetValueOrDefault());
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    return publication.Notes;
                }
            }
        }
       
        public string PublicationUrl
        {
            get
            {
                var publicationId = _uow.PublicationSectionRepository.Get(_assignment.Publication.Id)?.PublicationId;
                var publication = _uow.PublicationRepository.Get(publicationId.GetValueOrDefault());
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
