namespace TimeTrackingApp.Infrastructure.Models
{
    public class TrackingEventTypeDto
    {
        public Guid Id { get; init; }
        public string EventType { get; init; }
        public string? Description { get; init; }
    }
}
