using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class GetComments
    {
        private readonly CurationManagersComment _assignment;

        private readonly IUnitOfWork _uow;
        public GetComments(CurationManagersComment assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }
        public string CuratorComments
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.PublicationId)).FirstOrDefault();
                var comment =_uow.CuratorAssignmentRepository.Find(p=> p.PublicationId.Equals(publication.Id)).FirstOrDefault();
                return comment.Notes;
            }
        }
        public string ChiefCuratorComments
        {
            get
            {
                var publication = _uow.ChiefCuratorCommentRepository.Find(p => p.PublicationId.Equals(_assignment.PublicationId)).FirstOrDefault();
                return publication.Notes;
            }
        }
        public string PrincipalCuratorComments
        {
            get
            {
                var publication = _uow.PrincipalCuratorCommentRepository.Find(p => p.PublicationId.Equals(_assignment.PublicationId)).FirstOrDefault();
                return publication.Notes;
            }
        }
        public string CurationManagersComments
        {
            get
            {
                return _assignment.Notes;
            }
        }
    }
}
