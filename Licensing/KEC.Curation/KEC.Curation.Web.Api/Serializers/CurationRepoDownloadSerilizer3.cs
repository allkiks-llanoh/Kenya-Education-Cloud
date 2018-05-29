using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CurationRepoDownloadSerilizer3
    {
        private readonly Publication _assignment;

        private readonly IUnitOfWork _uow;

        public CurationRepoDownloadSerilizer3(Publication assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }

        public int PublicationId
        {
            get
            {
                var pub = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.Id)).FirstOrDefault();
                return pub.Id;
            }
        }
        public string KicdNumber
        {
            get
            {
                var pub = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.Id)).FirstOrDefault();
                return pub.KICDNumber;
            }
        }

        public string Title
        {
            get
            {
                var pub = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.Id)).FirstOrDefault();
                return pub.Title;
            }
        }
        public DateTime AssignmentDateUtc
        {
            get
            {
                var pub = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.Id)).FirstOrDefault();
                return pub.CreatedTimeUtc;
            }
        }
        public string Status
        {
            get
            {
                var pub = _uow.ChiefCuratorAssignmentRepository.Find(p => p.PublicationId.Equals(_assignment.Id)).FirstOrDefault();
                return pub.Submitted ? "Submitted" : "Pending"; ;
            }
        }
    }
}
