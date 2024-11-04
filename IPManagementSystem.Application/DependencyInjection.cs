using IPManagementSystem.Domain.Interfaces.service;
using Microsoft.Extensions.DependencyInjection;

namespace IPManagementSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IIPAddressService, IPAddressService>();
            return services;
        }
    }
}
