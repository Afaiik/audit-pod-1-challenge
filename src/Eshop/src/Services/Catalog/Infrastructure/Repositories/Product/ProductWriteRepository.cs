using Core.Entities;
using Core.Product;
using Infrastructure.Data;
using Infrastructure.Repositories.Base.Write;

namespace Infrastructure.Repositories.Product
{
    public class ProductWriteRepository : BaseWriteRepository<ProductEntity>, IProductWriteRepository
    {
        public ProductWriteRepository(CatalogDbContext context) : base(context) { }
    }
}
