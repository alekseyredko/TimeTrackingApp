using MediatR;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Commands.TimeTracks
{
    public class StopTimeTrackCommand: IRequest<TrackingEventDto>
    {         
        public DateTimeOffset TimeTrackStopTime { get; init; }
    }
}
