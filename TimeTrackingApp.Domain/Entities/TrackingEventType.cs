namespace TimeTrackingApp.Domain.Entities;

public class TrackingEventType : BaseEntity
{
    public ICollection<TrackingEvent> TrackingEvents { get; set; }
    public string EventType { get; set; }
    public string? Description { get; set; }

    public void AddTrackingEvent(TrackingEvent trackingEvent)
    {
        if (!TrackingEvents.Any(x => x.Id == trackingEvent.TrackingEventTypeId))
        {
            TrackingEvent timeTrack = new TrackingEvent();

            TrackingEvents.Add(timeTrack);
            return;
        }
        else
        {
            throw new InvalidOperationException("This tracking event has been already added!");
        }        
    }
}
