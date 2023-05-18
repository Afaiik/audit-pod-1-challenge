using Core.Base.Read;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base.Read
{
    public class BaseReadRepository<TEntity> : IBaseReadRepository<TEntity> where TEntity : class
    {
        internal readonly CatalogDbContext Context;
        internal readonly DbSet<TEntity> dbSet;

        public BaseReadRepository(CatalogDbContext context)
        {
            Context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await dbSet.ToListAsync();

        public virtual async Task<TEntity?> GetByIdAsync(int id) => await dbSet.FindAsync(id);
    }
}
