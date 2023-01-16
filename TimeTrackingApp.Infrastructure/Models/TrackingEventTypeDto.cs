namespace TimeTrackingApp.Infrastructure.Models
{
    public class TrackingEventTypeDto: Dto
    {       
        public string EventType { get; init; }
        public string? Description { get; init; }
        public ICollection<TrackingEventDto> TrackingEvents { get; init; } = new List<TrackingEventDto>();
    }
}
