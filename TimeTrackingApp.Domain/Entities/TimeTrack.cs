namespace TimeTrackingApp.Domain.Entities;

public class TimeTrack : BaseEntity
{
    public DateTimeOffset StartTrackTime { get; set; }

    public DateTimeOffset? EndTrackTime { get; set; }

    public TimeSpan? Duration { get; set; }

    public bool IsFinished { get; set; }

    public int TrackingEventId { get; set; }

    public TrackingEvent TrackingEvent { get; set; }

    public void StartTimeTrack()
    {
        StartTrackTime = DateTimeOffset.Now;
    }

    public void StopTimeTrack()
    {
        EndTrackTime= DateTimeOffset.Now;
        IsFinished = true;
        Duration = (EndTrackTime - StartTrackTime);
    }

    public void SetTrackingEvent(TrackingEvent trackingEvent)
    {
        TrackingEventId = trackingEvent.Id;
        TrackingEvent = trackingEvent;
    }
}