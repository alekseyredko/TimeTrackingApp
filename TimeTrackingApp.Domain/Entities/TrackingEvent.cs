namespace TimeTrackingApp.Domain.Entities;

public class TrackingEvent : BaseEntity
{
    public ICollection<TimeTrack> TimeTracks { get; set; }
    public string? Description { get; set; }
    public int TrackingEventTypeId { get; set; }
    public TrackingEventType TrackingEventType { get; set; }

    public void AddTimeTrack(int timeTrackId)
    {
        if (!TimeTracks.Any(x => x.Id == timeTrackId))
        {
            TimeTrack timeTrack = new TimeTrack();
            
            timeTrack.StartTimeTrack();

            TimeTracks.Add(timeTrack);
            return;
        }

        TimeTrack foundTimeTrack = TimeTracks.First(x => x.Id == timeTrackId);

        if (foundTimeTrack.IsFinished)
        {
            throw new InvalidOperationException("This time track has been already finished!");
        }

        foundTimeTrack.StopTimeTrack();        
    }
}