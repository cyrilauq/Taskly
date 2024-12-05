using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using TodoList.Application.IService;
using TodoList.Application.Services;

namespace TodoList.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddServiceOptions();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }

        public static IServiceCollection AddServiceOptions(this IServiceCollection services)
        {
            services.AddSingleton(Options.Create(new TokenOptions("3747D5F6-6420-4C9F-B140-39A54E77C3343747D5F6-6420-4C9F-B140-39A54E77C3343747D5F6-6420-4C9F-B140-39A54E77C334")));

            return services;
        }
    }
}
