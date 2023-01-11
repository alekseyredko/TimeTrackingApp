using MediatR;
using TimeTrackingApp.Application.UnitOfWork;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Commands.TrackingEvents
{
    public class CreateTrackingEventCommandHandler : IRequestHandler<CreateTrackingEventCommand, TrackingEventDto>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CreateTrackingEventCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<TrackingEventDto> Handle(CreateTrackingEventCommand request, CancellationToken cancellationToken)
        {
            IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);         

            TrackingEvent trackingEvent = 
                TrackingEvent.Create(request.Id, request.Name, request.Description, request.TrackingEventTypes.Select(x => TrackingEventType.Create(x.Id, x.EventType, x.Description)).ToArray());

            TrackingEvent addedTrackingEvent = await unitOfWork.TrackingEventRepository.AddTrackingEventAsync(trackingEvent, cancellationToken);

            await unitOfWork.SaveChangesAsync();

            return new TrackingEventDto
            {
                Id = addedTrackingEvent.Id,
                Name = addedTrackingEvent.Name,
                Description = addedTrackingEvent.Description,
                TimeTracks = addedTrackingEvent.TimeTracks.Select(x => new TimeTrackDto { Id = x.Id, StartTrackTime = x.StartTrackTime, EndTrackTime = x.EndTrackTime, Duration = x.Duration, IsFinished = x.IsFinished }).ToList().AsReadOnly(),
                TrackingEventTypes = addedTrackingEvent.TrackingEventTypes.Select(x => new TrackingEventTypeDto { Id = x.Id, EventType = x.EventType, Description = x.Description}).ToList().AsReadOnly()
            };
        }
    }
}
