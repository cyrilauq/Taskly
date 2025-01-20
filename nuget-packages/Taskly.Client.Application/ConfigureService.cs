using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taskly.Client.Application.Http.Handler;
using Taskly.Client.Application.Services;
using Taskly.Client.Application.Services.Interfaces;
using Taskly.Client.Application.State;
using Taskly.Client.Application.State.Interfaces;

namespace Taskly.Client.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices();
            services.AddApplicationStates();

            services.AddScoped<HttpAuthorizationHandler>();
            services.AddScoped<HttpStatusHandler>();

            services.AddHttpClient("TasklyHttpClient", cf => cf.BaseAddress = new Uri(configuration["ApiUrl"] ?? throw new Exception("No url given for the api")))
                .AddHttpMessageHandler<HttpAuthorizationHandler>()
                .AddHttpMessageHandler<HttpStatusHandler>();

            services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TasklyHttpClient"));

            services.AddMapper();

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<ITodoService, TodoService>();

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
