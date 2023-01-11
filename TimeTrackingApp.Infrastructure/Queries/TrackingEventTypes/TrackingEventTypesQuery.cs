using MediatR;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Queries.TrackingEventTypes
{
    public class TrackingEventTypesQuery: IRequest<IReadOnlyCollection<TrackingEventTypeDto>>
    {
    }
}
