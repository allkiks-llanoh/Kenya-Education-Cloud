using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PublicationDownloadSerilizer
    {
        private readonly Publication _publication;
        private readonly EFUnitOfWork _uow;

        public PublicationDownloadSerilizer(Publication publication, EFUnitOfWork uow)
        {
            _publication = publication;
            _uow = uow;

        }

        public string Title
        {
            get
            {
                return _publication.Title;
            }
        }
        public string ISBNNumber
        {
            get
            {
                return _publication.ISBNNumber;
            }
        }
        public string PublisherName
        {
            get
            {
                return _publication.PublisherName;
            }
        }
        public string AuthorName
        {
            get
            {
                return _publication.AuthorName;
            }
        }
        public decimal Price
        {
            get
            {
                return _publication.Price;
            }
        }
        public string Description
        {
            get
            {
                return _publication.Description;
            }
        }
        public int SubjectId
        {
            get
            {
                return _publication.SubjectId;
            }
        }
        public int LevelId
        {
            get
            {
                return _publication.LevelId;
            }
        }
       
    }
}
