
using KEC.Publishers.Data.Models;
using KEC.Publishers.Data.UnitOfWork.Core;

namespace KEC.Publishers.Api.Serializers
{
    public class PublisherContentDownloadSerilizer
    {
        private readonly Publication _assignment;

        private readonly IUnitOfWork _uow;

        public PublisherContentDownloadSerilizer(Publication assignment, IUnitOfWork uow)
        {
            _assignment = assignment;
            _uow = uow;
        }
        public int Id
        {
            get
            {
                return _assignment.Id;
            }
        }
        public string ContentNumber
        {

            get
            {
                if (_assignment.CertificateNumber == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _assignment.CertificateNumber;
                }

            }
        }
        public string Title
        {
            get
            {
                return _assignment.Title;
            }
        }
        public string Publisher
        {
            get
            {
                return _assignment.PublisherName;
            }
        }
        public string CurationUrl
        {
            get
            {
                return _assignment.CutationUrl == null ? string.Empty : _assignment.CutationUrl;
            }
        }
        public string AuthorName
        {
            get
            {
                return _assignment.AuthorName;
            }
        }
        public string Description
        {
            get
            {
                return _assignment.Description;
            }
        }
        public decimal UnitPrice
        {
            get
            {
                return _assignment.Price;
            }
        }
        public string Level
        {
            get
            {
                var level = _uow.LevelRepository.Get(_assignment.LevelId);
                return level.Name;
            }
        }
        public string Subject
        {
            get
            {
                var subject = _uow.SubjectRepository.Get(_assignment.SubjectId);
                return subject.Name;
            }
        }
        

    }
}
