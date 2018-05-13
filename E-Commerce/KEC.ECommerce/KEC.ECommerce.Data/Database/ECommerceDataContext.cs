using KEC.ECommerce.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.ECommerce.Data.Database
{
    public class ECommerceDataContext : DbContext
    {
        public ECommerceDataContext(DbContextOptions options) :
            base(options)
        {

        }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
