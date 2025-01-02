using Microsoft.Extensions.DependencyInjection;
using TodoList.Web.Application.Services;
using TodoList.Web.Application.Services.Interfaces;

namespace TodoList.Web.Application
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
