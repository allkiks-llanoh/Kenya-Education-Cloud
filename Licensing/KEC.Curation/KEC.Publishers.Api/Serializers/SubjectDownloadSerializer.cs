using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;

namespace KEC.Publishers.Api.Serializers
{
    public class SubjectDownloadSerializer
    {
        private readonly Subject _subject;
        private readonly IUnitOfWork _uow;
        public SubjectDownloadSerializer(Subject subject, IUnitOfWork uow)
        {
            _uow = uow;
            _subject = subject;
        }
        public int Id
        {
            get
            {
                return _subject.Id;
            }
        }
        public string Name
        {
            get
            {
                return _subject.Name;
            }
        }
        public int SubjectTypeId
        {
            get
            {
                return _subject.SubjectTypeId;
            }
        }
        public string SubjectType
        {
            get
            {
                return _uow.SubjectTypeRepository.Get(_subject.SubjectTypeId).Name;
            }
        }
        public DateTime CreatedAtUtc
        {
            get
            {
                return _subject.CreatedAtUtc;
            }
        }
        public DateTime UpdatedAtUtc
        {
            get
            {
                return _subject.UpdatedAtUtc;
            }
        }
    }
}
