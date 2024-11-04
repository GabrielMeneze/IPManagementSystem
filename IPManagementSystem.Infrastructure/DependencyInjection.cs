using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using IPManagementSystem.Infrastructure.Data;
using IPManagementSystem.Domain.Interfaces.repository;
using IPManagementSystem.Infrastructure.Repositories;

namespace IPManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["RedisSettings:ConnectionString"];
                options.InstanceName = "IPManagementSystem_";
            });

            services.AddScoped<IIPAddressRepository, IPAddressRepository>();
            return services;
        }
    }
}
