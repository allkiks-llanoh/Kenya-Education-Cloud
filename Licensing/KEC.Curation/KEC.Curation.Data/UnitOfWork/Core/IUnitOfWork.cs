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
       // PublicationStageRepository PublicationStageRepository { get; }
        PublicationStageLogRepository PublicationStageLogRepository  { get;}
        LevelRepository levelRepository { get; }
        CuratorRepository CuratorRepository { get; }
        CuratorTypeRepository CuratorTypeRepository { get; }
        GetCuratorsRepository GetCuratorsRepository { get; }
        SubjectCategoryRepository SubjectCategoryRepository { get; }
        int Complete();

    }
}
