using System.Threading;
using TimeTrackingApp.Core.Services.Interfaces;
using TimeTrackingApp.Core.UnitOfWork;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Domain.UnitOfWork;

namespace TimeTrackingApp.Core.Services
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public TimeTrackingService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<TimeTrack> GetCurrentActiveTimeTrackAsync(CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            return await unitOfWork.TimeTrackRepository.GetAsync(entity => !entity.IsFinished, cancellationToken, nameof(TimeTrack.TrackingEvent));
        }

        public async Task<TrackingEvent> GetActiveTrackingEventAsync(CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            return await unitOfWork.TrackingEventRepository.GetAsync(entity => entity.TimeTracks.Any(x => !x.IsFinished), cancellationToken, nameof(TrackingEvent.TimeTracks), nameof(TrackingEvent.TrackingEventType));
        }

        public async Task<ICollection<TrackingEvent>> GetTrackingEventsAsync(CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);
            return (await unitOfWork.TrackingEventRepository.GetListOfEntitiesAsync(cancellationToken, null, nameof(TrackingEvent.TrackingEventType))).ToList();
        }

        public async Task<ICollection<TrackingEventType>> GetAllTrackingEventTypes(CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);
            return (await unitOfWork.TrackingEventTypeRepository.GetListOfEntitiesAsync(cancellationToken, null, nameof(TrackingEventType.TrackingEvents))).ToList();
        }

        public async Task<TrackingEventType> AddTrackingEventTypeAsync(TrackingEventType trackingEventType, CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TrackingEventTypeRepository.AddAsync(trackingEventType, cancellationToken);

            await unitOfWork.SaveChangesAsync();

            return trackingEventType;
        }

        public async Task<TrackingEvent> AddTrakingEventAsync(TrackingEvent trackingEvent, CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TrackingEventRepository.AddAsync(trackingEvent, cancellationToken);

            await unitOfWork.SaveChangesAsync();

            return trackingEvent;
        }

        public async Task<TimeTrack> StartTimeTrackAsync(int trackingEventId, CancellationToken cancellationToken)
        {
            TimeTrack timeTrack = new TimeTrack { TrackingEventId = trackingEventId };
            timeTrack.StartTimeTrack();

            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TimeTrackRepository.AddAsync(timeTrack, cancellationToken);

            await unitOfWork.SaveChangesAsync();

            return timeTrack;
        }

        public async Task<TimeTrack> StopCurrentTimeTrackAsync(CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            TimeTrack timeTrack = await unitOfWork.TimeTrackRepository.GetAsync(entity => !entity.IsFinished, cancellationToken);

            timeTrack.StopTimeTrack();

            unitOfWork.TimeTrackRepository.Update(timeTrack);

            await unitOfWork.SaveChangesAsync();

            return timeTrack;
        }

        public async Task DeleteTrackingEventTypeAsync(int id, CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TrackingEventTypeRepository.RemoveAsync(id, cancellationToken);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTrackingEventAsync(int id, CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TrackingEventRepository.RemoveAsync(id, cancellationToken);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
