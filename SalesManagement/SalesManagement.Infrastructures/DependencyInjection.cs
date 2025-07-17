using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesManagement.Infrastructures.Persistence;

namespace SalesManagement.Infrastructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SalesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SalesManagement")));
            return services;
        }
    }
}
