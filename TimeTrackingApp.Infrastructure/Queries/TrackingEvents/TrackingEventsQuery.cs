using MediatR;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Queries.TrackingEvents;

public class TrackingEventsQuery: IRequest<IReadOnlyCollection<TrackingEventResponse>>
{
    
}