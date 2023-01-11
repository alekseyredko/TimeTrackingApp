using MediatR;
using TimeTrackingApp.Application.UnitOfWork;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Commands.TrackingEventTypes
{
    public class CreateTrackingEventTypeCommandHandler : IRequestHandler<CreateTrackingEventTypeCommand, TrackingEventTypeDto>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CreateTrackingEventTypeCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<TrackingEventTypeDto> Handle(CreateTrackingEventTypeCommand request, CancellationToken cancellationToken)
        {
            IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            TrackingEventType tracklingEventType = TrackingEventType.Create(request.Id, request.EventType, request.Description);

            await unitOfWork.TrackingEventTypeRepository.AddAsync(tracklingEventType, cancellationToken);

            await unitOfWork.SaveChangesAsync();

            return new TrackingEventTypeDto { Id = request.Id, EventType = request.EventType, Description= request.Description, TrackingEvents = new List<TrackingEventDto>() };
        }
    }
}
