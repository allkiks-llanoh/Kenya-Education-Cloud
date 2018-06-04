using System;
using System.Linq;
using KEC.Publishers.Data.Models;
using KEC.Publishers.Data.UnitOfWork.Core;

namespace KEC.Publishers.Api.Serializers
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
        public string Publisher
        {
            get
            {
                return _publication.PublisherName;
            }
        }
        public DateTime CompletionDate
        {
            get
            {
                return _publication.CompletionDate;
            }
        }
        public string ContentNumber
        {

            get
            {
                if (_publication.CertificateNumber == null)
                {
                    return "Not Yet Curated Succesfully";
                }
                else
                {
                    return _publication.CertificateNumber;
                }
            }
        }

    }
}
