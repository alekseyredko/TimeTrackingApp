using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTrackingApp.Core.Repositories;
using TimeTrackingApp.Application.UnitOfWork;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Infrastructure.Repositories;
using TimeTrackingApp.Persistence.Repositories;

namespace TimeTrackingApp.Infrastructure.UnitOfWork
{
    internal class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IUnitOfWork> CreateUnitOfWorkAsync(CancellationToken cancellationToken)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IDbContextFactory<ApplicationDbContext> dbContextFactory = _serviceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
            ApplicationDbContext applicationDbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            IGenericRepository<TimeTrack> timeTrackRepository = new GenericRepository<TimeTrack>(applicationDbContext);
            ITrackingEventRepository trackingEventRepository = new TrackingEventRepository(applicationDbContext);
            IGenericRepository<TrackingEventType> trackingEventTypeRepository = new GenericRepository<TrackingEventType>(applicationDbContext);

            return new UnitOfWork(timeTrackRepository, trackingEventRepository, trackingEventTypeRepository, applicationDbContext);
        }
    }
}
