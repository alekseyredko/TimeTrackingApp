using MediatR;
using TimeTrackingApp.Application.UnitOfWork;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Queries.TimeTracks
{
    public class TimeTracksQueryHandler : IRequestHandler<TimeTracksQuery, IReadOnlyCollection<TimeTrackDto>>
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public TimeTracksQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IReadOnlyCollection<TimeTrackDto>> Handle(TimeTracksQuery request, CancellationToken cancellationToken)
        {
            IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            IEnumerable<TimeTrack> timeTracks = await unitOfWork.TimeTrackRepository.GetListOfEntitiesAsync(cancellationToken);

            return timeTracks.Select(x => new TimeTrackDto { StartTrackTime = x.StartTrackTime, EndTrackTime = x.EndTrackTime, Duration = x.Duration, IsFinished = x.IsFinished, Id = x.Id }).ToList().AsReadOnly();
        }
    }
}
