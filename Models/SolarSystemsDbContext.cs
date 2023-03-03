using Microsoft.EntityFrameworkCore;

namespace SolarSystems.Models
{
    public class SolarSystemsDbContext : DbContext
    {
        public SolarSystemsDbContext(DbContextOptions<SolarSystemsDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
    }
}
