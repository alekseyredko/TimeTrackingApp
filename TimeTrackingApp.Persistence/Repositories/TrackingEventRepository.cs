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
            EntityEntry<TrackingEvent> entityEntry = await _applicationDbContext.AddAsync(trackingEvent);

            foreach (TrackingEventType trackingEventType in trackingEvent.TrackingEventTypes)
            {
                 _applicationDbContext.Attach<TrackingEventType>(trackingEventType);              
            }           
           
            return entityEntry.Entity;
        }
    }
}
