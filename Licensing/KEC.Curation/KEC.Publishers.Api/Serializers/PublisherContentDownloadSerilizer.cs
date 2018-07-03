
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
        
    }
}
