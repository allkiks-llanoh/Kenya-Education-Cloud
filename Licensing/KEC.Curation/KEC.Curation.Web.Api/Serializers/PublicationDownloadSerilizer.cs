using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Linq;
using KEC.Curation.Services.Extensions;
using System.ComponentModel.DataAnnotations;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PublicationDownloadSerilizer
    {
        private readonly Publication _publication;
        private readonly IUnitOfWork _uow;

        public PublicationDownloadSerilizer(Publication publication, IUnitOfWork uow)
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
         
        
        public bool ChiefCuratorCanProcess
        {
            get
            {
                return _uow.PublicationRepository.CanProcessCurationPublication(_publication);
            }
        }
        public string ChiefCuratorActionTaken
        {
            get
            {
                var curationStageLog = _uow.PublicationStageLogRepository.Find(p => p.Stage == PublicationStage.Curation && p.Id.Equals(_publication.Id)).FirstOrDefault();
                return curationStageLog == null ? string.Empty : curationStageLog.Stage.GetDescription();
            }
        }
        public string ChiefCuratorComment
        {
            get
            {
             var stageLog =   _uow.PublicationStageLogRepository.Find(p => p.Stage == PublicationStage.Curation).FirstOrDefault();
                return stageLog == null ? string.Empty : stageLog.Notes;
                
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
