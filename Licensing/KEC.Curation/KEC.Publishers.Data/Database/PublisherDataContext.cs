using KEC.Publishers.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Publishers.Data.Database
{
    public class PublisherDataContext:DbContext
    {
        public PublisherDataContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChiefCuratorAssignment>()
                .HasOne(a => a.Publication)
                .WithOne(p => p.ChiefCuratorAssignment)
                .HasForeignKey<Publication>(p => p.ChiefCuratorAssignmentId);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CuratorAssignment>()
                .HasOne(a => a.PublicationSection)
                .WithOne(p => p.ChiefCuratorAssignment)
                .HasForeignKey<PublicationSection>(p => p.ChiefCuratorAssignmentId);
        }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectType> SubjectTypes { get; set; }
        public DbSet<PublicationSection> PublicationSections { get; set; }
        public DbSet<PublicationStageLog> PublicationStageLogs { get; set; }
        public DbSet<CuratorAssignment> CuratorAssignments { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<ChiefCuratorAssignment> ChiefCuratorAssignments { get; set; }
    }
}
