namespace TimeTrackingApp.Domain.Entities;

public class TrackingEvent : BaseEntity
{
    private List<TimeTrack> _timeTracks = new List<TimeTrack>();
    private List<TrackingEventType> _trackingEventTypes= new List<TrackingEventType>();

    public IReadOnlyCollection<TimeTrack> TimeTracks => _timeTracks.AsReadOnly();
    
    public string Name { get; private set; }

    public string? Description { get; private set; }  

    public bool IsTracking { get; private set; }
    
    public IReadOnlyCollection<TrackingEventType> TrackingEventTypes => _trackingEventTypes.AsReadOnly();  

    private TrackingEvent(){}

    public static TrackingEvent Create(Guid id, string name, string? description, params TrackingEventType[] trackingEventTypes)
    {
        //Rule to validate string length
        if (description?.Length > 5000)
        {
            throw new Exception("Description is too long.");
        }

        TrackingEvent trackingEvent = CreatePrivate(id, name, description, trackingEventTypes);

        if (trackingEventTypes?.Length > 0)
        {            
            foreach (TrackingEventType trackingEventType in trackingEventTypes)
            {
                trackingEventType.AddTrackingEvent(trackingEvent);
            }
        }
        else
        {
            // if no Tracking Event types, add standart one
            TrackingEventType defaultTrakingEventType = TrackingEventType.Create(Guid.Empty, "default", null);
            defaultTrakingEventType.AddTrackingEvent(trackingEvent);
            trackingEvent.AddTrackingEventTypes(defaultTrakingEventType);
        }
        return trackingEvent;
    }

    private static TrackingEvent CreatePrivate(Guid id, string name, string? description, params TrackingEventType[] trackingEventTypes)
    {
        TrackingEvent trackingEvent = new TrackingEvent { Id = id, Name = name, Description = description };
        trackingEvent.AddTrackingEventTypes(trackingEventTypes);

        return trackingEvent;
    }

    private void AddTrackingEventTypes(params TrackingEventType[] trackingEventTypes)
    {
        if (_trackingEventTypes == null)
        {
            _trackingEventTypes = new List<TrackingEventType>();
        }
        _trackingEventTypes.AddRange(trackingEventTypes);
    }

    public void StartTimeTrack(DateTimeOffset startDateTimeOffset)
    {
        //TODO: finish previous timetrack?
        //TODO: use guid instead of id?
        TimeTrack timeTrack = TimeTrack.Create(Guid.NewGuid(), startDateTimeOffset, this);
        _timeTracks.Add(timeTrack);
        IsTracking= true;
    }

    public void StopCurentTimeTrack(DateTimeOffset stopDateTimeOffset)
    {
        TimeTrack timeTrackToFinish = _timeTracks.First(timeTrack => !timeTrack.IsFinished);
        timeTrackToFinish.StopTimeTrack(stopDateTimeOffset);
        IsTracking= false;
    }
}