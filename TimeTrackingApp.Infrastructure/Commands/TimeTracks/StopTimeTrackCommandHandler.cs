using MediatR;
using TimeTrackingApp.Application.UnitOfWork;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Commands.TimeTracks
{
    public class StopTimeTrackCommandHandler : IRequestHandler<StopTimeTrackCommand, TrackingEventDto>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public StopTimeTrackCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<TrackingEventDto> Handle(StopTimeTrackCommand request, CancellationToken cancellationToken)
        {
            IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            TrackingEvent trackingEvent = await unitOfWork.TrackingEventRepository.GetAsync(x => x.IsTracking, cancellationToken, nameof(TrackingEvent.TimeTracks));

            trackingEvent.StopCurentTimeTrack(request.TimeTrackStopTime);

            unitOfWork.TrackingEventRepository.UpdateTrackingEvent(trackingEvent);

            await unitOfWork.SaveChangesAsync();

            return new TrackingEventDto
            {
                Id = trackingEvent.Id,
                Name = trackingEvent.Name,
                Description = trackingEvent.Description,
                IsTracking = trackingEvent.IsTracking,
                TimeTracks = trackingEvent.TimeTracks
                    .Select(x => 
                        new TimeTrackDto
                        {
                            Id = x.Id,
                            StartTrackTime = x.StartTrackTime,
                            EndTrackTime= x.EndTrackTime,
                            Duration = x.Duration,
                            IsFinished = x.IsFinished,
                        })
                    .ToList()
                    .AsReadOnly()
            };
        }
    }
}
