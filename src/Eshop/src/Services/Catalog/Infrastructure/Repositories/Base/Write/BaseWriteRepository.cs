using Core.Interfaces.Repositories.WriteRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base.Write
{
    public class BaseWriteRepository<TEntity> : IBaseWriteRepository<TEntity> where TEntity : class
    {
        internal readonly CatalogDbContext Context;
        internal readonly DbSet<TEntity> dbSet;

        public BaseWriteRepository(CatalogDbContext context)
        {
            Context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity) => await dbSet.AddAsync(entity);

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities) => await dbSet.AddRangeAsync(entities);

        public virtual void Remove(TEntity entity) => dbSet.Remove(entity);

        public virtual void RemoveRange(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            dbSet.AttachRange(entitiesToUpdate);
            Context.Entry(entitiesToUpdate).State = EntityState.Modified;
        }
    }
}
