namespace TimeTrackingApp.Domain.Entities;

public class TrackingEventType : BaseEntity
{
    private List<TrackingEvent> _trackingEvents = new List<TrackingEvent>();

    public IReadOnlyCollection<TrackingEvent> TrackingEvents => _trackingEvents.AsReadOnly();
    public string EventType { get; private set; }
    public string? Description { get; private set; }

    private TrackingEventType(Guid id, string eventType, string? description)
    {
        Id = id;
        EventType = eventType;
        Description = description;
    }

    public static TrackingEventType Create(Guid id, string eventType, string? description)
    {
        if (description?.Length > 5000)
        {
            throw new Exception("Descrption must be shorter than 5000 symbols");
        }

        return new TrackingEventType(id, eventType, description);
    }

    internal void AddTrackingEvent(TrackingEvent trackingEvent)
    {
        _trackingEvents.Add(trackingEvent);
    }
}
