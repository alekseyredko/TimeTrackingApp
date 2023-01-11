namespace TimeTrackingApp.Infrastructure.Models
{
    public class TrackingEventDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public bool IsTracking { get; init; }
        public IReadOnlyCollection<TimeTrackDto> TimeTracks { get; init; } = new List<TimeTrackDto>().AsReadOnly();
        public IReadOnlyCollection<TrackingEventTypeDto> TrackingEventTypes { get; init; } = new List<TrackingEventTypeDto>().AsReadOnly();
    }
}