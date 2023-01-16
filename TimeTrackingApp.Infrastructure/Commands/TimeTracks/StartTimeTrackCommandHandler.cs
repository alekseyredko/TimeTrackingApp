using MediatR;
using TimeTrackingApp.Application.UnitOfWork;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Commands.TimeTracks
{
    public class StartTimeTrackCommandHandler : IRequestHandler<StartTimeTrackCommand, TrackingEventDto>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public StartTimeTrackCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<TrackingEventDto> Handle(StartTimeTrackCommand request, CancellationToken cancellationToken)
        {
            IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            TrackingEvent trackingEvent = await unitOfWork.TrackingEventRepository.GetAsync(x => x.Id == request.TrackingEventId, cancellationToken, nameof(TrackingEvent.TimeTracks));

            trackingEvent.StartTimeTrack(request.TimeTrackStartTime);

            unitOfWork.TrackingEventRepository.UpdateTrackingEvent(trackingEvent);

            await unitOfWork.SaveChangesAsync();

            return new TrackingEventDto
            {
                Id = request.TrackingEventId,
                Name = trackingEvent.Name,
                Description = trackingEvent.Description,
                IsTracking= trackingEvent.IsTracking,
                TimeTracks = trackingEvent.TimeTracks
                    .Select(x => 
                        new TimeTrackDto 
                        { 
                            Id = x.Id, 
                            StartTrackTime = x.StartTrackTime, 
                            EndTrackTime = x.EndTrackTime, 
                            Duration = x.Duration,
                            IsFinished = x.IsFinished 
                        })
                    .ToList()
                    .AsReadOnly()
            };
        }
    }
}
