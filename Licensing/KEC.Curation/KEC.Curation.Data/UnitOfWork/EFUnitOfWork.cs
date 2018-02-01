using System;
using KEC.Curation.Data.Database;
using KEC.Curation.Data.Repositories;

namespace KEC.Curation.Data.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly CurationDataContext _context;
        public EFUnitOfWork()
        {
            // new context
        }
        public PublicationRepository PublicationRepository =>  new PublicationRepository(_context);

        public PublicationSectionRepository PublicationSectionRepository => new PublicationSectionRepository(_context);

        public SubjectRepository SubjectRepository => new SubjectRepository(_context);

        public SubjectTypeRepository SubjectTypeRepository => new SubjectTypeRepository(_context);

        public CuratorAssignmentRepository CuratorAssignmentRepository => new CuratorAssignmentRepository(_context);

        public PublicationStageRepository PublicationStageRepository => new PublicationStageRepository(_context);

        PublicationStageLogRepository IUnitOfWork.PublicationStageLogRepository => new PublicationStageLogRepository(_context);

       public  int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
