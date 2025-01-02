using Microsoft.Extensions.DependencyInjection;
using Taskly.Web.Application.Services;
using Taskly.Web.Application.Services.Interfaces;

namespace Taskly.Web.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddApplicationServices();

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
