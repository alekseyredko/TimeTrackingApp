using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimeTrackingApp.Core.Repositories;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly DbSet<TEntity> _entities;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _entities = _applicationDbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _entities.AddAsync(entity, cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            if (_applicationDbContext.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }
            _entities.Remove(entity);
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            TEntity entity = await _entities.FindAsync(new object[] { id, }, cancellationToken: cancellationToken);
            Remove(entity);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken, params string[] incliudeProperties)
        {           
            IQueryable<TEntity> query = _entities.AsQueryable();

            foreach (string propertyToInclude in incliudeProperties)
            {
                query = _entities.Include(propertyToInclude);
            }

            return await query.FirstOrDefaultAsync(filter, cancellationToken);            
        }

        public async Task<IEnumerable<TEntity>> GetListOfEntitiesAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> filter = null, params string[] incliudeProperties)
        {
            IQueryable<TEntity> query = _entities.AsQueryable();
            if (filter != null)
            {
                query = _entities.Where(filter);
            }           

            if (incliudeProperties.Length != 0)
            {
                foreach (string includeProperty in incliudeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
        }     
    }
}
