using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Linq;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CurationDownloadSerializer
    {
        private readonly CuratorAssignment _assignment;

        private readonly IUnitOfWork _uow;

        public CurationDownloadSerializer(CuratorAssignment assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }
        public DateTime AssignmentDateUtc
        {
            get
            {
                return _assignment.CreatedUtc;
            }
        } 
        public string Publication
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return publication.KICDNumber;
            }
        }
        public string SectionToCurate
        {
            get
            {
                var section = _uow.PublicationSectionRepository.Get(_assignment.PublicationSectionId)?.SectionDescription;
                return section ;
            }
        }
        public int AssignmentId
        {
            get
            {
                return _assignment.Id;
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
                return _assignment.Notes;
            }
        }
        public string Assignee
        {
            get
            {
                return _assignment.Assignee;
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
                var publicationId = _uow.PublicationSectionRepository.Get(_assignment.PublicationSectionId)?.PublicationId;
                var publication = _uow.PublicationRepository.Get(publicationId.GetValueOrDefault());
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    return publication.CutationUrl;
                }
            }
        }
    }
}
