namespace TimeTrackingApp.Infrastructure.Models
{
    public class TimeTrackResponse
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartTrackTime { get; set; }

        public DateTimeOffset? EndTrackTime { get; set; }

        public TimeSpan? Duration { get; set; }

        public bool IsFinished { get; set; }
    }
}