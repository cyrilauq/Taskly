using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.IService;

namespace TodoList.Application.Services
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
