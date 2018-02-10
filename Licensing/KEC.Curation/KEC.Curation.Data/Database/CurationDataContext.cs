using System;
using KEC.Curation.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KEC.Curation.Data.Database
{
    public class CurationDataContext : DbContext
    {
        public CurationDataContext(DbContextOptions options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectType> SubjectTypes { get; set; }
        public DbSet<PublicationSection> PublicationSections { get; set; }     
        public DbSet<PublicationStageLog> PublicationStageLogs { get; set; }
        public DbSet<CuratorAssignment> CuratorAssignments { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<CuratorType> CuratorTypes { get; set; }
        public DbSet<CuratorCreation> CuratorCreations { get; set; }
        public DbSet<SubjectCategory> SubjectCategories { get; set; }
        internal void Update()
        {
            throw new NotImplementedException();
        }
    }
}
