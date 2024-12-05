using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TodoList.Application.IService;
using TodoList.Application.Services;

namespace TodoList.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITokenService, TokenService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddServiceOptions(configuration);
            services.AddHttpContextAccessor();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }

        public static IServiceCollection AddServiceOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<TokenOptions>().BindConfiguration(TokenOptions.TokenOptionsKey);

            return services;
        }
    }
}
