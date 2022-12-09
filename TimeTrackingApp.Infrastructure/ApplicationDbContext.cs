using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TimeTrack> TimeTracks { get; set; }
        public DbSet<TrackingEvent> TrackingEvents { get; set; }
        public DbSet<TrackingEventType> TrackingEventTypes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TimeTrack>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TrackingEvent>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TrackingEventType>()
                .HasKey(t => t.Id);

            //modelBuilder.Entity<TimeTrack>()                
            //    .HasOne<TrackingEvent>()
            //    .WithMany()
            //    .HasForeignKey(t => t.TrackingEventId)
            //    .HasPrincipalKey(t => t.Id);

            //modelBuilder.Entity<TrackingEvent>()
            //    .HasOne<TrackingEventType>()
            //    .WithMany()
            //    .HasForeignKey(t => t.TrackingEventTypeId)
            //    .HasPrincipalKey(t => t.Id);

            modelBuilder.Entity<TrackingEventType>()
                .HasMany(t => t.TrackingEvents)
                .WithOne(t => t.TrackingEventType);

            modelBuilder.Entity<TrackingEvent>()
                .HasMany(t => t.TimeTracks)
                .WithOne(t => t.TrackingEvent);
        }
    }
}
