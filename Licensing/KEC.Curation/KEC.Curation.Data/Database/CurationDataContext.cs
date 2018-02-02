using KEC.Curation.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KEC.Curation.Data.Database
{
    public class CurationDataContext : DbContext
    {
        public CurationDataContext(DbContextOptions options)
            :base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectType> SubjectTypes { get; set; }
        public DbSet<PublicationSection> PublicationSections { get; set; }
        public DbSet<PublicationStage> PublicationStages { get; set; }
        public DbSet<PublicationStageLog> PublicationStageLogs { get; set; }
        public DbSet<CuratorAssignment> CuratorAssignments { get; set; }
        public DbSet<Level> Levels { get; set; }

    }
}
