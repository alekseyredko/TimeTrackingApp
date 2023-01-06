using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Core.Repositories;

public interface ITrackingEventRepository: IGenericRepository<TrackingEvent>
{
    Task<TrackingEvent> AddTrackingEventAsync(TrackingEvent trackingEvent, CancellationToken cancellation);
}
