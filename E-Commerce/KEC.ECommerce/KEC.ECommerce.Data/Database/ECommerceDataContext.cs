using KEC.ECommerce.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KEC.ECommerce.Data.Database
{
    public class ECommerceDataContext : DbContext
    {
        public ECommerceDataContext(DbContextOptions options) :
            base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasOne(a => a.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);
            
        }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Licence> Licences { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PurchasedBook> PurchasedBooks { get; set; }
    }
}
