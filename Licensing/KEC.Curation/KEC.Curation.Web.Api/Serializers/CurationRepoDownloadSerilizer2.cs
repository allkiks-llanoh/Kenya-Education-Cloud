using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CurationRepoDownloadSerilizer2
    {
        private readonly CuratorAssignment _assignment;

        private readonly IUnitOfWork _uow;

        public CurationRepoDownloadSerilizer2(CuratorAssignment assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }

        public int PublicationId
        {
            get
            {
                var pub = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return pub.Id;
            }
        }
        public string KicdNumber
        {
            get
            {
                var pub = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return pub.KICDNumber;
            }
        }

        public string Title
        {
            get
            {
                var pub = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return pub.Title;
            }
        }
        public DateTime AssignmentDateUtc
        {
            get
            {
                var pub = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                return pub.CreatedTimeUtc;
            }
        }
    }
}
