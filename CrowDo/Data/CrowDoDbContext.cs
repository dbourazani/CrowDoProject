using Microsoft.EntityFrameworkCore;

namespace CrowDo.Core.Data
{
    public class CrowDoDbContext : DbContext
    {
        private readonly string connectionString_;

        public CrowDoDbContext()
        {
            connectionString_ =
                "Server =localhost; Database =crowDo3; " +
                "Integrated Security=SSPI;Persist Security Info=False;";
        }

        public CrowDoDbContext(string connString)
        {
            connectionString_ = connString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<CrowDo.Models.User>()
                .ToTable("User", "core");

            modelBuilder
                .Entity<CrowDo.Models.User>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder
                .Entity<CrowDo.Models.FundingPackage>()
                .ToTable("FundingPackage", "core");    
          
            modelBuilder
                .Entity<CrowDo.Models.Project>()
                .ToTable("Project", "core");

            modelBuilder
                .Entity<CrowDo.Models.Project>()
                .HasIndex(c => c.Title)
                .IsUnique();

        }
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
