using Microsoft.Extensions.DependencyInjection;

namespace CloudSales.Infrastructure.Ccp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCcpClient(this IServiceCollection services)
        {
            services.AddScoped<ICcpClient, CcpClient>();
            return services;
        }
    }
}
