using Infrastructure.Repositories.Product;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories.Base
{
    public static class BaseRepositoriesConfigurator
    {
        public static void ConfigureRepositories(this IServiceCollection service)
        {
            service.ConfigureProductRepository();
        }
    }
}
