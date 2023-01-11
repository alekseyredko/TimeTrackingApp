using MediatR;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Commands.TrackingEventTypes
{
    public class CreateTrackingEventTypeCommand: IRequest<TrackingEventTypeDto>
    {
        public Guid Id { get; init; }
        public string EventType { get; init; }
        public string? Description { get; init; }
    }
}
