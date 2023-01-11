using MediatR;
using TimeTrackingApp.Application.UnitOfWork;

namespace TimeTrackingApp.Infrastructure.Commands.TrackingEventTypes
{
    public class DeleteTrackingEventTypeCommandHandler : IRequestHandler<DeleteTrackingEventTypeCommand, Unit>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public DeleteTrackingEventTypeCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Unit> Handle(DeleteTrackingEventTypeCommand request, CancellationToken cancellationToken)
        {
            IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TrackingEventTypeRepository.RemoveAsync(request.Id, cancellationToken);

            await unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
