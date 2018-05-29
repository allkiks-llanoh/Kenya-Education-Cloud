using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KEC.ECommerce.Data.Database
{
    /// <summary>
    /// Provides a factory class to be used by the IOC to inject
    /// Ecommerce Datacontext
    /// </summary>
    public class ECommerceDataContextFactory : IDesignTimeDbContextFactory<ECommerceDataContext>
    {
        public ECommerceDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("Database.json").Build();
            var connectionString = configuration.GetConnectionString("ECommerceDatabase");
            optionsBuilder.UseSqlServer(connectionString);
            return new ECommerceDataContext(optionsBuilder.Options);

        }
    }
}
