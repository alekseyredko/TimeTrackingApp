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

            return await unitOfWork.TrackingEventRepository.GetAsync(entity => entity.TimeTracks.Any(x => !x.IsFinished), cancellationToken, nameof(TrackingEvent.TimeTracks), nameof(TrackingEvent.TrackingEventTypes));
        }

        public async Task<List<TrackingEvent>> GetTrackingEventsAsync(CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);
            return (await unitOfWork.TrackingEventRepository.GetListOfEntitiesAsync(cancellationToken, null, nameof(TrackingEvent.TrackingEventTypes))).ToList();
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

            TrackingEvent addedTrackingEvemt = await unitOfWork.TrackingEventRepository.AddTrackingEventAsync(trackingEvent, cancellationToken);

            await unitOfWork.SaveChangesAsync();

            return addedTrackingEvemt;
        }

        public async Task<TimeTrack> StartTimeTrackAsync(Guid trackingEventId, DateTimeOffset startTime, CancellationToken cancellationToken)
        {          
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            TrackingEvent trackingEvent = await unitOfWork.TrackingEventRepository.GetAsync(x => x.Id == trackingEventId, cancellationToken , nameof(TrackingEvent.TimeTracks));

            trackingEvent.StartTimeTrack(startTime);

            await unitOfWork.SaveChangesAsync();

            return await this.GetCurrentActiveTimeTrackAsync(cancellationToken);            
        }

        public async Task<TimeTrack> StopCurrentTimeTrackAsync(DateTimeOffset endTime, CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            TrackingEvent trackingEvent = await unitOfWork.TrackingEventRepository.GetAsync(x => x.IsTracking, cancellationToken, nameof(TrackingEvent.TimeTracks));

            trackingEvent.StopCurentTimeTrack(endTime);            

            await unitOfWork.SaveChangesAsync();

            return trackingEvent.TimeTracks.Last();
        }

        public async Task DeleteTrackingEventTypeAsync(Guid id, CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TrackingEventTypeRepository.RemoveAsync(id, cancellationToken);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTrackingEventAsync(Guid id, CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TrackingEventRepository.RemoveAsync(id, cancellationToken);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTimeTrackAsync(Guid id, CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TimeTrackRepository.RemoveAsync(id, cancellationToken);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<List<TimeTrack>> GetFinishedTimeTracksAsync(CancellationToken cancellationToken)
        {
            using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);
            return (await unitOfWork.TimeTrackRepository.GetListOfEntitiesAsync(cancellationToken, entity => entity.IsFinished, nameof(TimeTrack.TrackingEvent))).ToList();
        }
    }
}
