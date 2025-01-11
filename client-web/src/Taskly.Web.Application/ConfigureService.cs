using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Taskly.Web.Application.Http.Handler;
using Taskly.Web.Application.Mappers;
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

            services.AddScoped<HttpStatusHandler>();

            services.AddHttpClient("TasklyHttpClient", cf => cf.BaseAddress = new Uri(configuration["ApiUrl"] ?? throw new Exception("No url given for the api")))
                .AddHttpMessageHandler<HttpStatusHandler>();

            services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TasklyHttpClient"));

            services.AddMapper();

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

        private static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
