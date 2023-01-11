using MediatR;
using TimeTrackingApp.Application.UnitOfWork;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Queries.TrackingEventTypes
{
    public class TrackingEventTypesQueryHandler : IRequestHandler<TrackingEventTypesQuery, IReadOnlyCollection<TrackingEventTypeDto>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public TrackingEventTypesQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IReadOnlyCollection<TrackingEventTypeDto>> Handle(TrackingEventTypesQuery request, CancellationToken cancellationToken)
        {
            IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            IEnumerable<TrackingEventType> trackingEventTypes = await unitOfWork.TrackingEventTypeRepository.GetListOfEntitiesAsync(cancellationToken, null, nameof(TrackingEventType.TrackingEvents));

            return trackingEventTypes.Select(x => 
                new TrackingEventTypeDto 
                { 
                    Id = x.Id, 
                    Description = x.Description,
                    EventType = x.EventType, 
                    TrackingEvents = x.TrackingEvents
                        .Select(y =>
                            new TrackingEventDto 
                            { 
                                Id = y.Id, 
                                Description = y.Description,
                                Name = y.Name, 
                                IsTracking = y.IsTracking 
                            }).ToList().AsReadOnly() 
                })
                .ToList()
                .AsReadOnly();
        }
    }
}
