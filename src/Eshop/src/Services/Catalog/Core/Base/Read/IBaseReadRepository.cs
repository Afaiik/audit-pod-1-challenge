namespace Core.Base.Read
{
    public interface IBaseReadRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

    }
}
