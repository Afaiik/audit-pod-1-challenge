using Core.Entities;
using Core.Product;
using Infrastructure.Data;
using Infrastructure.Repositories.Base.Read;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Product
{
    public class ProductReadRepository : BaseReadRepository<ProductEntity>, IProductReadRepository
    {
        public ProductReadRepository(CatalogDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductEntity>> GetWithPagination(string searchCriteria, int pageSize, int pageIndex)
        {
            return await dbSet.Where(x =>
                    x.Description.Trim().ToLower().StartsWith(searchCriteria.Trim().ToLower())
                )
                .AsNoTracking()
                .OrderBy(x => x.Description)
                .Skip(CalculateSkipSize(pageSize, pageIndex))
                .Take(pageSize)
                .ToListAsync();
        }

        private int CalculateSkipSize(int pageSize, int pageIndex) => pageIndex > 1 ? (pageSize * pageIndex) - pageSize : 0;
    }
}
