using Microsoft.Extensions.DependencyInjection;
using Services.Product;

namespace Services.Base
{
    public static class BaseServicesConfigurator
    {
        public static void ConfigureServices(this IServiceCollection service)
        {
            service.ConfigureProductService();
        }
    }
}
