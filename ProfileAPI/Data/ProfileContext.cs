using Microsoft.EntityFrameworkCore;
using ProfileAPI.Models;

namespace ProfileAPI.Data
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options) { }

        public DbSet<Info> Info { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Info>(entity =>
            {
                entity.HasKey(e => e.Email);
            });
            
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.DialCode);
            });
        }
    }
}
