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

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<SolarSystems.Models.Component> Component { get; set; }

        public DbSet<SolarSystems.Models.Project> Project { get; set; }

        public DbSet<SolarSystems.Models.Container>? Container { get; set; }
    }
}
