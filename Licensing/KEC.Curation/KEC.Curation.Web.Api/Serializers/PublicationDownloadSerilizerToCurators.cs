using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Linq;
using KEC.Curation.Services.Extensions;
using System.ComponentModel.DataAnnotations;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PublicationDownloadSerilizerToCurators
    {
        private readonly Publication _publication;
        private readonly IUnitOfWork _uow;

        public PublicationDownloadSerilizerToCurators(Publication publication, IUnitOfWork uow)
        {
            _publication = publication;
            _uow = uow;

        }
        public int Id
        {
            get
            {
                return _publication.Id;
            }
        }

        public string Url
        {
            get
            {
                return _publication.Url;
            }
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
        public string KICDNumber
        {
            get
            {
                return _publication.KICDNumber;
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

        public string Subject
        {
            get
            {
                var subject = _uow.SubjectRepository.Get(_publication.SubjectId);
                return subject.Name;
            }
        }
        public string Type
        {
            get
            {
                return _publication.MimeType;
            }
        }
        public string Level
        {
            get
            {
                var level = _uow.LevelRepository.Get(_publication.LevelId);
                return level.Name;
            }
        }
        public DateTime CompletionDate
        {
            get
            {
                return _publication.CompletionDate;
            }
        }

        public string Publisher
        {
            get
            {
                var publication = _uow.PublicationRepository.Find(p => p.Id.Equals(_publication.Id)).FirstOrDefault();
                return publication.PublisherName;
            }
        }
        public string ContentNumber
        {
            get
            {
                var contentNumber = _uow.PublicationRepository.Find(p => p.Id.Equals(_publication.Id)).FirstOrDefault();
                return contentNumber == null ? string.Empty : contentNumber.CertificateNumber;
            }
        }
        public string CurationUrl
        {
            get
            {
                return _publication.CutationUrl == null ? string.Empty : _publication.CutationUrl;
            }
        }
    }
}
