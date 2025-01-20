using Microsoft.Extensions.DependencyInjection;
using Taskly.Client.Domain.Repositories.Interfaces;
using Taskly.Client.Infrastructure.Repository;

namespace Taskly.Natif.Infrastructure
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
