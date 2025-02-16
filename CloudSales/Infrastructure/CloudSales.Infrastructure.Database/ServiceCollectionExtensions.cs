using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CloudSales.Infrastructure.Database
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresqlConnection");
            services.AddScoped(x => new NpgsqlConnection(connectionString));
            return services;
        }
    }
}
