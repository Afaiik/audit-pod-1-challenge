using Core.Product;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Product
{
    public static class ProductServiceConfigurator
    {
        public static void ConfigureProductService(this IServiceCollection service)
        {
            service.AddScoped<IProductService, ProductService>();
        }
    }
}
