namespace Core.Interfaces.Repositories.WriteRepositories
{
    public interface IBaseWriteRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entityToUpdate);

        void UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
    }
}
