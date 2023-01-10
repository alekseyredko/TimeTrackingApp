using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Application.Services.Interfaces
{
    public interface ITimeTrackingService
    {
        Task<TrackingEventType> AddTrackingEventTypeAsync(TrackingEventType trackingEventType, CancellationToken cancellationToken);
        Task<TrackingEvent> AddTrakingEventAsync(TrackingEvent trackingEvent, CancellationToken cancellationToken);
        Task DeleteTimeTrackAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteTrackingEventAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteTrackingEventTypeAsync(Guid id, CancellationToken cancellation);
        Task<TrackingEvent> GetActiveTrackingEventAsync(CancellationToken cancellationToken);
        Task<ICollection<TrackingEventType>> GetAllTrackingEventTypes(CancellationToken cancellationToken);
        Task<TimeTrack> GetCurrentActiveTimeTrackAsync(CancellationToken cancellationToken);
        Task<List<TimeTrack>> GetFinishedTimeTracksAsync(CancellationToken cancellationToken);
        Task<List<TrackingEvent>> GetTrackingEventsAsync(CancellationToken cancellationToken);
        Task<TimeTrack> StartTimeTrackAsync(Guid trackingEventId, DateTimeOffset startTime, CancellationToken cancellationToken);
        Task<TimeTrack> StopCurrentTimeTrackAsync(DateTimeOffset endTime, CancellationToken cancellationToken);
    }
}