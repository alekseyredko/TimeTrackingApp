namespace TimeTrackingApp.Application.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        Task<IUnitOfWork> CreateUnitOfWorkAsync(CancellationToken cancellationToken);
    }
}