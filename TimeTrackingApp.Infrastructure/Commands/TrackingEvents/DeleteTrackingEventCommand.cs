using MediatR;

namespace TimeTrackingApp.Infrastructure.Commands.TrackingEvents
{
    public class DeleteTrackingEventCommand: IRequest<Unit>
    {
        public Guid Id { get; init; }
    }
}
