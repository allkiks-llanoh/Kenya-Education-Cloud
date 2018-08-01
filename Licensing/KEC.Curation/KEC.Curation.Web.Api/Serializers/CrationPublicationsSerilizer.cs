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
        public string PublicationUrl
        {
            get
            {
                var pub = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                var url = pub.CutationUrl;
                if (url == null)
                {
                    return string.Empty;
                }
                else
                {
                    return pub.CutationUrl;
                }
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
        public string ChiefCuratorGuid
        {
            get
            {
                return _assignment.ChiefCuratorGuid;
            }
        }

    }
}
