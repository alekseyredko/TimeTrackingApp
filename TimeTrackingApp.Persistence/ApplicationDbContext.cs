using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<TimeTrack>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.StartTrackTime);
                b.Property(x => x.EndTrackTime);
                b.Property(x => x.Duration);
                b.Property(x => x.IsFinished);                
                b.Navigation(nameof(TimeTrack.TrackingEvent));                
            });

            modelBuilder.Entity<TrackingEvent>(b => 
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name);
                b.Property(x => x.Description);
                b.Property(x => x.IsTracking);                    
                b.Navigation(x => x.TrackingEventTypes);
            });

            modelBuilder.Entity<TrackingEventType>(b => 
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.EventType);
                b.Property(x => x.Description);
                b.Navigation(x => x.TrackingEvents);
            });              
        }
    }
}
