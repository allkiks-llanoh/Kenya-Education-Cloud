using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class ChiefCuratorFindCuratedPublicationsSerilizer
    {
        private readonly CuratorAssignment _assignment;

        private readonly IUnitOfWork _uow;
        public ChiefCuratorFindCuratedPublicationsSerilizer(CuratorAssignment assignment, IUnitOfWork uow)
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
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                var section = _uow.PublicationSectionRepository.Find(p => p.PublicationId.Equals(publication.Id)).FirstOrDefault();

                if (section == null)
                {
                    return "Whole Document";
                }
                else
                {
                    return section.SectionDescription;
                }
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

        public string Title
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return publication.Title;
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
                    return publication.CutationUrl;
                }
            }
        }
        public string Assignee
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                var _assignee = _uow.CuratorAssignmentRepository.Find(p => p.PublicationId.Equals(publication.Id)).FirstOrDefault();
                if (_assignee == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _assignee.Assignee;
                }

            }
        }
    }
}
