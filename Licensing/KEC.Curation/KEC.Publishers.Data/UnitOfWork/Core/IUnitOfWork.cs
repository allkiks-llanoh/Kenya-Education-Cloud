using KEC.Publishers.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Publishers.Data.UnitOfWork.Core
{
    public interface IUnitOfWork : IDisposable
    {
        PublicationRepository PublicationRepository { get; }
        SubjectRepository SubjectRepository { get; }
        SubjectTypeRepository SubjectTypeRepository { get; }
        LevelRepository LevelRepository { get; }
        PublicationStageLogRepository PublicationStageLogRepository { get; }
        int Complete();
    }
}
