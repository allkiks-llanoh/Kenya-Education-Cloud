using KEC.ECommerce.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.UnitOfWork.Core
{
    public interface IUnitOfWork : IDisposable
    {
        PublicationsRepository PublicationsRepository { get; }
        PublishersRepository PublishersRepository { get; }
        LevelsRepository LevelsRepository { get; }
        AuthorsRepository AuthorsRepository { get; }
        SubjectsRepository SubjectsRepository { get; }
        int Complete();
    }
}
