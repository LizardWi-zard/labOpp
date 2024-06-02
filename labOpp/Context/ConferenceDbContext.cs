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
                .HasMany(u => u.Applications)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Drafts)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserID);

            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Applications)
                .WithOne(app => app.Activity)
                .HasForeignKey(app => app.ActivityTypeID);

            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Drafts)
                .WithOne(d => d.Activity)
                .HasForeignKey(d => d.ActivityTypeID);
        }
    }
}
