using labOpp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace labOpp.Context
{
    public class ConferenceDbContext : DbContext
    {
        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationDraft> ApplicationDrafts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Applications);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Drafts)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserID);

            modelBuilder.Entity<Activity>();
        }
    }
}
