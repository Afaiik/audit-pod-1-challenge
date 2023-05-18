using Core.Models.Product;

namespace Core.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetWithPagination(string searchCriteria, int pageSize, int pageIndex);
    }
}
