using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Curation.Data.Database
{
    public class CurationContextFactory : IDesignTimeDbContextFactory<IUnitOfWork>
    {
        public IUnitOfWork CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("Database.json").Build();
            var connectionString = configuration.GetConnectionString("CurationDatabase");
            optionsBuilder.UseSqlServer(connectionString);
            return new IUnitOfWork(optionsBuilder.Options);
        }
    }
}
