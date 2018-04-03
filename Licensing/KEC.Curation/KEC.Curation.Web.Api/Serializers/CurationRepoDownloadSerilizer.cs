using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.ComponentModel.DataAnnotations;



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
                //var publicationId = _uow.PublicationSectionRepository.Get(_assignment.PublicationId)?.PublicationId;
                //var publication = _uow.PublicationRepository.Get(publicationId.GetValueOrDefault());
                //if (publication == null)
                //{
                //    return string.Empty;
                //}
                //else
                //{
                //    return publication.Url;
                return _assignment.Publication.Url;

                //}
            }
        }
    }
}
