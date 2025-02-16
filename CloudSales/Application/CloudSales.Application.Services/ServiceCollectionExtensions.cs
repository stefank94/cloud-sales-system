using Microsoft.Extensions.DependencyInjection;

namespace CloudSales.Application.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<ISoftwareService, SoftwareService>();
            return services;
        } 
    }
}
