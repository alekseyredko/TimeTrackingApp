using MediatR;
using TimeTrackingApp.Application.UnitOfWork;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Queries.TrackingEvents
{
    public class TrackingEventsQueryHandler : IRequestHandler<TrackingEventsQuery, IReadOnlyCollection<TrackingEventResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public TrackingEventsQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IReadOnlyCollection<TrackingEventResponse>> Handle(TrackingEventsQuery request, CancellationToken cancellationToken)
        {
            IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            IEnumerable<TrackingEvent> trackigEvents = await unitOfWork.TrackingEventRepository.GetListOfEntitiesAsync(cancellationToken, incliudeProperties: nameof(TrackingEvent.TimeTracks));

            return trackigEvents.Select(x => new TrackingEventResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                TimeTracks = x.TimeTracks.Select(y => new TimeTrackResponse
                {
                    Id = y.Id,
                    StartTrackTime = y.StartTrackTime,
                    EndTrackTime = y.EndTrackTime,
                    Duration = y.Duration,
                    IsFinished = y.IsFinished
                }).ToList().AsReadOnly()
            })
                .ToList()
                .AsReadOnly();
        }
    }
}