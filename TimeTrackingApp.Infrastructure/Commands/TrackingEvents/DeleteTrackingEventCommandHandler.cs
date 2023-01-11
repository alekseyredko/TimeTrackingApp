using MediatR;
using TimeTrackingApp.Application.UnitOfWork;

namespace TimeTrackingApp.Infrastructure.Commands.TrackingEvents
{
    public class DeleteTrackingEventCommandHandler : IRequestHandler<DeleteTrackingEventCommand, Unit>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public DeleteTrackingEventCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Unit> Handle(DeleteTrackingEventCommand request, CancellationToken cancellationToken)
        {
            IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync(cancellationToken);

            await unitOfWork.TrackingEventRepository.RemoveAsync(request.Id, cancellationToken);

            await unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
