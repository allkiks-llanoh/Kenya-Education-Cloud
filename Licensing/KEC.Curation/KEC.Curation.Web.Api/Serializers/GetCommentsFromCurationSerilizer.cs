using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class GetCommentsFromCurationSerilizer
    {
        private readonly Publication _assignment;

        private readonly IUnitOfWork _uow;
        public GetCommentsFromCurationSerilizer(Publication assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }
        public string CuratorComments
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_assignment.Id)).FirstOrDefault();
                var comment = _uow.CuratorAssignmentRepository.Find(p => p.PublicationId.Equals(publication.Id)).FirstOrDefault();
                return comment.Notes;
            }
        }
        public string ChiefCuratorComments
        {
            get
            {
                var publication = _uow.ChiefCuratorCommentRepository.Find(p => p.PublicationId.Equals(_assignment.Id)).FirstOrDefault();
                return publication.Notes;
            }
        }
        public string PrincipalCuratorComments
        {
            get
            {
                var publication = _uow.PrincipalCuratorCommentRepository.Find(p => p.PublicationId.Equals(_assignment.Id)).FirstOrDefault();
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
        public string CutationManagersCuratorComments
        {
            get
            {
                var publication = _uow.CurationManagersCommentRepository.Find(p => p.PublicationId.Equals(_assignment.Id)).FirstOrDefault();
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

    }
}
