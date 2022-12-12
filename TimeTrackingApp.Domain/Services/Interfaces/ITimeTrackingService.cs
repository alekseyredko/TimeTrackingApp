using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Core.Services.Interfaces
{
    public interface ITimeTrackingService
    {
        Task<TrackingEventType> AddTrackingEventTypeAsync(TrackingEventType trackingEventType, CancellationToken cancellationToken);
        Task<TrackingEvent> AddTrakingEventAsync(TrackingEvent trackingEvent, CancellationToken cancellationToken);
        Task DeleteTimeTrackAsync(int id, CancellationToken cancellationToken);
        Task DeleteTrackingEventAsync(int id, CancellationToken cancellationToken);
        Task DeleteTrackingEventTypeAsync(int id, CancellationToken cancellation);
        Task<TrackingEvent> GetActiveTrackingEventAsync(CancellationToken cancellationToken);
        Task<ICollection<TrackingEventType>> GetAllTrackingEventTypes(CancellationToken cancellationToken);
        Task<TimeTrack> GetCurrentActiveTimeTrackAsync(CancellationToken cancellationToken);
        Task<List<TimeTrack>> GetFinishedTimeTracksAsync(CancellationToken cancellationToken);
        Task<List<TrackingEvent>> GetTrackingEventsAsync(CancellationToken cancellationToken);
        Task<TimeTrack> StartTimeTrackAsync(int trackingEventId, CancellationToken cancellationToken);
        Task<TimeTrack> StopCurrentTimeTrackAsync(CancellationToken cancellationToken);
    }
}