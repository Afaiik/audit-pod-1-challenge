using Catalog.API.Controllers.Base;
using Core.Exceptions;
using Core.Models.Base;
using Core.Models.Product;
using Core.Product;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    /// <summary>
    /// Controller for catalog
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : BaseApiController<IProductService>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public CatalogController(IProductService service) : base(service) { }

        /// <summary>
        /// Retrieves a list of products according to the search criteria and pagination provided
        /// </summary>
        /// <param name="searchCriteria">Criteria</param>
        /// <param name="pageSize">PageSize</param>
        /// <param name="pageIndex">Page index hello world</param> 
        /// <returns></returns>
        [HttpGet(nameof(GetWithPagination))]
        public async Task<ResponseDto<IEnumerable<ProductDto>>> GetWithPagination(string searchCriteria, int pageSize, int pageIndex)
        {
            throw new AnyException();
            return await ExecuteAsync(() => _service.GetWithPagination(searchCriteria, pageSize, pageIndex));
        }

        /// <summary>
        /// Retrieves a list of products according to the search criteria and pagination provided
        /// </summary>
        /// <param name="searchCriteria">Criteria</param>
        /// <param name="pageSize">PageSize</param>
        /// <param name="pageIndex">Page index hello world</param> 
        /// <returns></returns>
        [HttpGet(nameof(GetWithOutPagination))]
        public async Task<ResponseDto<IEnumerable<ProductDto>>> GetWithOutPagination(string searchCriteria, int pageSize, int pageIndex)
        {
            throw new AnyException();
            return await ExecuteAsync(() => _service.GetWithPagination(searchCriteria, pageSize, pageIndex));
        }
    }
}
