using MediatR;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Commands.TimeTracks
{
    public class StartTimeTrackCommand: IRequest<TrackingEventDto>
    {
        public Guid TrackingEventId { get; init; }
        public DateTimeOffset TimeTrackStartTime { get; init; }
    }
}
