using KEC.Curation.Data.Repositories;
using System;

namespace KEC.Curation.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        PublicationRepository PublicationRepository { get; }
        PublicationSectionRepository PublicationSectionRepository { get; }
        SubjectRepository SubjectRepository { get; }
        SubjectTypeRepository SubjectTypeRepository { get; }
        CuratorAssignmentRepository CuratorAssignmentRepository { get; }
        PublicationStageLogRepository PublicationStageLogRepository  { get;}
        LevelRepository LevelRepository { get; }
        int Complete();
    }
}
