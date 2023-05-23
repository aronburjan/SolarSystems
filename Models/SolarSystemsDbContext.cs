using Microsoft.EntityFrameworkCore;
using SolarSystems.Models;

namespace SolarSystems.Models
{
    public class SolarSystemsDbContext : DbContext
    {
        public SolarSystemsDbContext(DbContextOptions<SolarSystemsDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<SolarSystems.Models.Component> Component { get; set; }

        public DbSet<SolarSystems.Models.Project> Project { get; set; }

        public DbSet<SolarSystems.Models.Container>? Container { get; set; }

        public DbSet<SolarSystems.Models.ProjectStatus> ProjectStatus { get; set; }

        public DbSet<SolarSystems.Models.ProjectComponent> ProjectComponent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectComponent>()
                  .HasKey(m => new { m.ProjectId, m.ComponentId });
        }
    }
}
