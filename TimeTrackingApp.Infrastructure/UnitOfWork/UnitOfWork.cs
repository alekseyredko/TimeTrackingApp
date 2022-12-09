using TimeTrackingApp.Application.Repositories;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Domain.UnitOfWork;

namespace TimeTrackingApp.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;

        private readonly IGenericRepository<TimeTrack> _timeTrackRepository;

        private readonly IGenericRepository<TrackingEvent> _trackingEventRepository;

        private readonly IGenericRepository<TrackingEventType> _trackingEventTypeRepository;

        private readonly ApplicationDbContext _applicationDbContext;

        public IGenericRepository<TimeTrack> TimeTrackRepository
            => _timeTrackRepository;

        public IGenericRepository<TrackingEvent> TrackingEventRepository
            => _trackingEventRepository;

        public IGenericRepository<TrackingEventType> TrackingEventTypeRepository
            => _trackingEventTypeRepository;


        public UnitOfWork(
            IGenericRepository<TimeTrack> timeTrackRepository,
            IGenericRepository<TrackingEvent> trackingEventRepository,
            IGenericRepository<TrackingEventType> trackingEventTypeRepository,
            ApplicationDbContext applicationDbContext)
        {
            _timeTrackRepository = timeTrackRepository;
            _trackingEventRepository = trackingEventRepository;
            _trackingEventTypeRepository = trackingEventTypeRepository;
            _applicationDbContext = applicationDbContext;
        }

        public void SaveChanges()
            => _applicationDbContext.SaveChanges();

        public async Task SaveChangesAsync()
            => await _applicationDbContext.SaveChangesAsync();

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            // Dispose of unmanaged resources.
            await DisposeAsync(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _applicationDbContext.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                await _applicationDbContext.DisposeAsync();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }
    }
}
