using KEC.Publishers.Data.Database;
using KEC.Publishers.Data.Repositories;
using KEC.Publishers.Data.UnitOfWork.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KEC.Publishers.Data.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly PublisherDataContext _context;
        public EFUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("Database.json").Build();
            var connectionString = configuration.GetConnectionString("CurationDatabase");
            optionsBuilder.UseSqlServer(connectionString);
            _context = new Database.PublisherDataContext(optionsBuilder.Options);
        }
        public PublicationRepository PublicationRepository => new PublicationRepository(_context);
        public SubjectRepository SubjectRepository => new SubjectRepository(_context);
        public SubjectTypeRepository SubjectTypeRepository => new SubjectTypeRepository(_context);
        public LevelRepository LevelRepository => new LevelRepository(_context);
        public PublicationStageLogRepository PublicationStageLogRepository => new PublicationStageLogRepository(_context);
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
