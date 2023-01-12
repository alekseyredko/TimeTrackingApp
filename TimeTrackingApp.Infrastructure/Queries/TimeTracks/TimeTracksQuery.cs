using MediatR;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Queries.TimeTracks
{
    public class TimeTracksQuery: IRequest<IReadOnlyCollection<TimeTrackDto>>
    {
        
    }
}
