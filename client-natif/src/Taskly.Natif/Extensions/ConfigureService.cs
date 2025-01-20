using Microsoft.Extensions.Configuration;
using System.Reflection;
using Taskly.Client.Application;
using Taskly.Natif.Infrastructure;
using Taskly.Natif.Pages;
using Taskly.Natif.ViewModels;
using Taskly.Natif.ViewModels.Interfaces;

namespace Taskly.Natif.Extensions
{
    internal static class ConfigureService
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInfrastructure()
                .ConfigureApplicationServices(configuration)
                .AddViewModels()
                .AddViews();

            return services;
        }

        private static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<LoginViewModel>();

            return services;
        }

        private static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddTransient<LoginPage>();

            return services;
        }

        public static IConfiguration AddSettingsConfiguration(this ConfigurationManager configuration)
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("Taskly.Natif.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            configuration.AddConfiguration(config);
            return configuration;
        }
    }
}
