namespace TimeTrackingApp.Infrastructure.Models
{
    public class TrackingEventDto: Dto
    {        
        public string Name { get; init; }
        public string? Description { get; init; }
        public bool IsTracking { get; init; }
        public ICollection<TimeTrackDto> TimeTracks { get; init; } = new List<TimeTrackDto>();
        public ICollection<TrackingEventTypeDto> TrackingEventTypes { get; init; } = new List<TrackingEventTypeDto>();
    }
}