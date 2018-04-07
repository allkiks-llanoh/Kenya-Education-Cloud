using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class ChiefCutatorDownloadSerilizer
    {
        private readonly ChiefCuratorAssignment _assignment;

        private readonly IUnitOfWork _uow;
        public ChiefCutatorDownloadSerilizer(ChiefCuratorAssignment assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }
        public DateTime AssignmentDateUtc
        {
            get
            {
                return _assignment.AssignmetDateUtc;
            }
        }
        public string Publication
        {
            get
            {
                //var publicationId = _uow.ChiefCuratorAssignmentRepository.Get(_assignment.Publication.)?.PublicationId;
                //var publicationRecord = _uow.PublicationRepository.Get(publicationId.GetValueOrDefault());
                //var subject = _uow.SubjectRepository.Get(publicationRecord.SubjectId).Name;
                //var recordinfo = $"Subject: {subject} KICD Number: {publicationRecord.KICDNumber}";
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return publication.KICDNumber;
            }
        }
        public string SectionToCurate
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                var section = _uow.PublicationSectionRepository.Find(p=> p.PublicationId.Equals(publication.Id)).FirstOrDefault();
                return section.SectionDescription;
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
