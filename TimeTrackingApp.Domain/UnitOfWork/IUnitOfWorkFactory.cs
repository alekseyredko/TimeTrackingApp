using TimeTrackingApp.Domain.UnitOfWork;

namespace TimeTrackingApp.Core.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        Task<IUnitOfWork> CreateUnitOfWorkAsync(CancellationToken cancellationToken);
    }
}