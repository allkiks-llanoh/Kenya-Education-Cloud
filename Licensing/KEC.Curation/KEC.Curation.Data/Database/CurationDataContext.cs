using KEC.Curation.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KEC.Curation.Data.Database
{
    public class CurationDataContext : DbContext
    {
        public CurationDataContext(DbContextOptions options)
            :base(options)
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
        public DbSet<ChiefCuratorComment> ChiefCuratorComments{ get; set; }

    }
}
