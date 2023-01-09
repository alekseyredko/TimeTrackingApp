using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TimeTrackingApp.Core.Repositories;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Infrastructure;
using TimeTrackingApp.Infrastructure.Repositories;

namespace TimeTrackingApp.Persistence.Repositories
{
    internal class TrackingEventRepository : GenericRepository<TrackingEvent>, ITrackingEventRepository
    {      
        public TrackingEventRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {         
        }

        public async Task<TrackingEvent> AddTrackingEventAsync(TrackingEvent trackingEvent, CancellationToken cancellation)
        {           
            EntityEntry<TrackingEvent> entityEntry = _applicationDbContext.Entry<TrackingEvent>(trackingEvent);
            entityEntry.State = EntityState.Added;

            foreach (TrackingEventType trackingEventType in trackingEvent.TrackingEventTypes)
            {
                EntityEntry<TrackingEventType> trackingEventTypeEntry = _applicationDbContext.Entry<TrackingEventType>(trackingEventType);
                if (trackingEventType.Id == Guid.Empty)
                {
                    trackingEventTypeEntry.State = EntityState.Added;
                }
                else
                {
                    trackingEventTypeEntry.State = EntityState.Unchanged;
                }
            }

            return entityEntry.Entity;
        }

        public TrackingEvent UpdateTrackingEvent(TrackingEvent trackingEvent)
        {
            EntityEntry<TrackingEvent> entityEntry = _applicationDbContext.Attach(trackingEvent);

            foreach (TimeTrack timeTrack in trackingEvent.TimeTracks)
            {
                if (timeTrack.EndTrackTime.HasValue)
                {
                    _applicationDbContext.Attach<TimeTrack>(timeTrack);
                }
                else
                {
                    _applicationDbContext.Add<TimeTrack>(timeTrack);
                }
            }

            return entityEntry.Entity;
        }
    }
}
