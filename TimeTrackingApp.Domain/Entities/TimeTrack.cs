namespace TimeTrackingApp.Domain.Entities;

public partial class TimeTrack : BaseEntity
{
    public DateTimeOffset StartTrackTime { get; private set; }

    public DateTimeOffset? EndTrackTime { get; private set; }

    public TimeSpan? Duration { get; private set; }

    public bool IsFinished { get; private set; } 

    public TrackingEvent TrackingEvent { get; private set; }
    
    internal static TimeTrack Create(Guid timeTrackId, DateTimeOffset startTrackTime, TrackingEvent trackingEvent)
    {
        return new TimeTrack { Id = timeTrackId, StartTrackTime = startTrackTime, TrackingEvent = trackingEvent };
    }    

    internal void StopTimeTrack(DateTimeOffset endTrackTime)
    {
        if(endTrackTime < StartTrackTime)
        {
            throw new Exception("EndTrackTime cannot be lower than StartTrackTime.");
        }

        EndTrackTime = endTrackTime;
        IsFinished = true;
        Duration = (EndTrackTime - StartTrackTime);
    }
}