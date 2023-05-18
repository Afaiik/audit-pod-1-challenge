using Core.Base.Read;
using Core.Entities;

namespace Core.Product
{
    public interface IProductReadRepository : IBaseReadRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> GetWithPagination(string searchCriteria, int pageSize, int pageIndex);
    }
}
