using AutoMapper;
using Core.Entities;
using Core.Models.Product;
using Core.Product;

namespace Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        //TODO: DI
        private readonly IMapper _mapper;

        public ProductService(IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetWithPagination(string searchCriteria, int pageSize, int pageIndex)
        {
            //Why models on core layer ?
            if (!ValidateGetPageSize(pageSize)) //Validator for method instead of entity ? 
                throw new ArgumentOutOfRangeException("PageSize can only be 5 - 10 - 15 - 20"); //TODO: ErrorMessages resx ??

            var items = await _productReadRepository.GetWithPagination(searchCriteria, pageSize, pageIndex);
            return items.Select(x => _mapper.Map<ProductEntity, ProductDto>(x));
        }

        private bool ValidateGetPageSize(int pageSize) => pageSize % 5 == 0 && pageSize <= 20 && pageSize > 0;
    }
}
