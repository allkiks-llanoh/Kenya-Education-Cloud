using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CurationRepoDownloadSerilizer
    {
        private readonly CuratorAssignment _assignment;

        private readonly IUnitOfWork _uow;

        public CurationRepoDownloadSerilizer(CuratorAssignment assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }

        public int PublicationId
        {
            get
            {
                return _assignment.PublicationId;
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

        [DataType(DataType.Html)]
        public string Notes
        {
            get
            {

                return _assignment.Notes;

            }
        }

        public string PublicationUrl
        {
            get
            {
                var publicationId = _uow.CuratorAssignmentRepository.Get(_assignment.PublicationId)?.PublicationId;
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
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
        public string Publication
        {
            get
            {
                var publicationId = _uow.CuratorAssignmentRepository.Get(_assignment.PublicationId)?.PublicationId;
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                if (publication == null)
                {
                    return string.Empty;
                }
                else
                {
                    return publication.KICDNumber;
                }
            }
        }
        public string SectionToCurate
        {
            get
            {
                var section = _uow.CuratorAssignmentRepository.Find(p => p.PublicationId.Equals(_assignment.PublicationId)
                && p.PublicationSectionId.Equals(_assignment.Id)).FirstOrDefault();
               
                if (section == null)
                {
                    return "Whole Document";
                }
                else
                {
                    return section.PublicationSection.SectionDescription;
                }
                
            }
        }
        public DateTime AssignmentDateUtc
        {
            get
            {
                return _assignment.CreatedUtc;
            }
        }
    }
}
