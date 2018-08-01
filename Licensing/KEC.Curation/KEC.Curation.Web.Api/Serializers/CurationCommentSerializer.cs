using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CurationCommentSerializer
    {
        private readonly ChiefCuratorComment _assignment;

        private readonly IUnitOfWork _uow;

        public CurationCommentSerializer(ChiefCuratorComment assignment, IUnitOfWork uow)
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
       
       
        public string Status
        {
            get
            {
                return _assignment.Submitted ? "Submitted" : "Pending";
            }
        }
        public string Recommendation
        {
            get
            {
                return _assignment.Recomendation;
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
                var publicationId = _uow.PublicationSectionRepository.Get(_assignment.PublicationId)?.PublicationId;
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
        public string FullName
        {
            get
            {
                var entry = _uow.ChiefCuratorCommentRepository.Find(p => p.PublicationId.Equals(_assignment.PublicationId)).FirstOrDefault();

                if (entry == null)
                {
                    return "";
                }
                else
                {
                    return entry.FullName;
                }
            }

        }
    }
}
