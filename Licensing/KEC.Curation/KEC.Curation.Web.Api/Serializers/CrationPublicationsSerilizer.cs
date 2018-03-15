using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CrationPublicationsSerilizer
    {
        private readonly ChiefCuratorAssignment _assignment;

        private readonly IUnitOfWork _uow;

        public CrationPublicationsSerilizer(ChiefCuratorAssignment assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }
       
        public string Publication
        {
            get
            {
                var publicationId = _uow.ChiefCuratorAssignmentRepository.Get(_assignment.PublicationSection.PublicationId)?.PublicationId;
                var publicationRecord = _uow.PublicationRepository.Get(publicationId.GetValueOrDefault());
                var subject = _uow.SubjectRepository.Get(publicationRecord.SubjectId).Name;
                var recordinfo = $"Subject: {subject} KICD Number: {publicationRecord.KICDNumber}";
                return recordinfo;
            }
        }
       
        public string Status
        {
            get
            {
                return _assignment.Submitted ? "Submitted" : "Pending";
            }
        }
       
        public string Assignee
        {
            get
            {
                return _assignment.ChiefCuratorGuid;
            }
        }
        public int id
        {
            get
            {
                return _assignment.Publication.Id;
            }
        }


    }
}
