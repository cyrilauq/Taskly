using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taskly.Web.Application.Http;
using Taskly.Web.Application.Http.Client;
using Taskly.Web.Application.Services;
using Taskly.Web.Application.Services.Interfaces;
using Taskly.Web.Application.State;
using Taskly.Web.Application.State.Interfaces;

namespace Taskly.Web.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices();
            services.AddApplicationStates();

            services.AddScoped<HttpClient>(factory => new TasklyHttpClient() { BaseAddress = new Uri(configuration["ApiUrl"] ?? throw new Exception("No url given for the api")) });

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }

        private static IServiceCollection AddApplicationStates(this IServiceCollection services)
        {
            services.AddSingleton<IAuthState, AuthState>();

            return services;
        }
    }
}
