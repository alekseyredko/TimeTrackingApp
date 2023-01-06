using System.Linq.Expressions;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Core.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetListOfEntitiesAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null, params string[] incliudeProperties);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellation, params string[] incliudeProperties);    
    Task AddAsync(TEntity entity, CancellationToken cancellation);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task RemoveAsync(Guid id, CancellationToken cancellationToken);
}
