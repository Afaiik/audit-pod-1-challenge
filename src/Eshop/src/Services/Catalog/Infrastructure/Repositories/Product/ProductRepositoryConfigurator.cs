using Core.Product;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories.Product
{
    public static class ProductRepositoryConfigurator
    {
        public static void ConfigureProductRepository(this IServiceCollection service)
        {
            service.AddScoped<IProductReadRepository, ProductReadRepository>();
            service.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
