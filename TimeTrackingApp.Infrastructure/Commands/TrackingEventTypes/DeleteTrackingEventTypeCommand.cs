using MediatR;

namespace TimeTrackingApp.Infrastructure.Commands.TrackingEventTypes
{
    public class DeleteTrackingEventTypeCommand: IRequest<Unit>
    {
        public Guid Id { get; init; }
    }
}
