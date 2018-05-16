using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Repositories;
using KEC.ECommerce.Data.UnitOfWork.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDataContext _context;

        public EFUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("Database.json").Build();
            var connectionString = configuration.GetConnectionString("ECommerceDatabase");
            optionsBuilder.UseSqlServer(connectionString);
            _context = new ECommerceDataContext(optionsBuilder.Options);
        }
        public PublicationsRepository PublicationsRepository => new PublicationsRepository(_context);

        public PublishersRepository PublishersRepository => new PublishersRepository(_context);

        public LevelsRepository LevelsRepository => new LevelsRepository(_context);

        public AuthorsRepository AuthorsRepository => new AuthorsRepository(_context);

        public SubjectsRepository SubjectsRepository => new SubjectsRepository(_context);

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
