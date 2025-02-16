using CloudSales.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CloudSales.Infrastructure.Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IPurchasedSoftwareRepository, PurchasedSoftwareRepository>();
            services.AddScoped<ISoftwareRepository, SoftwareRepository>();
            services.AddScoped<ICache, Cache>();
            return services;
        }
    }
}
