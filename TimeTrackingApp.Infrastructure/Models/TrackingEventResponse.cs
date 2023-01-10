namespace TimeTrackingApp.Infrastructure.Models
{
    public class TrackingEventResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsTracking { get; set; }
        public IReadOnlyCollection<TimeTrackResponse> TimeTracks { get; set; } = new List<TimeTrackResponse>().AsReadOnly();
    }
}