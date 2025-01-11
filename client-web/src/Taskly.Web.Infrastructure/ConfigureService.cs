using Microsoft.Extensions.DependencyInjection;
using Taskly.Web.Infrastructure.Repositories;
using Taskly.Web.Infrastructure.Repositories.Interfaces;

namespace Taskly.Web.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services)
        {
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoApiRepository>();

            return services;
        }
    }
}
