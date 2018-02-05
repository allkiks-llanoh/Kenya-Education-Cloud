﻿using KEC.Curation.Data.Database;
using KEC.Curation.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KEC.Curation.Data.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly Database.IUnitOfWork _context;
        public EFUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("Database.json").Build();
            var connectionString = configuration.GetConnectionString("CurationDatabase");
            optionsBuilder.UseSqlServer(connectionString);
            _context = new Database.IUnitOfWork(optionsBuilder.Options);
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
