using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TimeTrackingApp.Core.Services.Interfaces;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Domain.UnitOfWork;
using TimeTrackingApp.Infrastructure.UnitOfWork;

namespace Playground
{
    public class HostedService : IHostedService
    {
        private readonly ITimeTrackingService _timeTrackingService;
        private readonly ILogger<HostedService> _logger;

        public HostedService(ITimeTrackingService timeTrackingService, ILogger<HostedService> logger)
        {
            _timeTrackingService = timeTrackingService;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            TrackingEventType trackingEventType = new TrackingEventType
            {
                EventType = "Test1",
                Description = "Test type"
            };

            TrackingEventType trackingEventType1 = await _timeTrackingService.AddTrackingEventTypeAsync(trackingEventType, cancellationToken);

            TrackingEvent trackingEvent = new TrackingEvent
            {
                TrackingEventTypeId = trackingEventType.Id,
            };

            TrackingEvent trackingEvent1 = await _timeTrackingService.AddTrakingEventAsync(trackingEvent, cancellationToken);

            TimeTrack timeTrack = await _timeTrackingService.StartTimeTrackAsync(trackingEvent1.Id, cancellationToken);

            await Task.Delay(1000);

            TrackingEvent trackingEvent2 = await _timeTrackingService.GetActiveTrackingEventAsync(cancellationToken);

            TimeTrack timeTrack2 = await _timeTrackingService.GetCurrentActiveTimeTrackAsync(cancellationToken);

            TimeTrack timeTrack1 = await _timeTrackingService.StopCurrentTimeTrackAsync(cancellationToken);
        }
     
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
