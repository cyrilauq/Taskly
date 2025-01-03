using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taskly.Web.Application.Http;
using Taskly.Web.Application.Http.Client;
using Taskly.Web.Application.Services;
using Taskly.Web.Application.Services.Interfaces;

namespace Taskly.Web.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices();
            services.AddScoped<HttpClient>(factory => new TasklyHttpClient() { BaseAddress = new Uri(configuration["ApiUrl"] ?? throw new Exception("No url given for the api")) });

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
