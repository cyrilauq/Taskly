using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TodoList.Application.Services;
using TodoList.Application.Services.Interfaces;

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
            services.AddScoped<IUserContext, UserContext>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddMappers();

            return services;
        }

        public static IServiceCollection AddServiceOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<TokenOptions>().BindConfiguration(TokenOptions.TokenOptionsKey);

            return services;
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ConfigureServices).Assembly);

            return services;
        }
    }
}
