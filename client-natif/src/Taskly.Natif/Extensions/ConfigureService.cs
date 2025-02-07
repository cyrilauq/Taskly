using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Taskly.Client.Application;
using Taskly.Natif.Application.Services.Interface;
using Taskly.Natif.Infrastructure;
using Taskly.Natif.Pages;
using Taskly.Natif.Services;
using Taskly.Natif.ViewModels;

namespace Taskly.Natif.Extensions
{
    internal static class ConfigureService
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInfrastructure()
                .ConfigureApplicationServices(configuration)
                .AddViewModels()
                .AddViews()
                .AddNatifServices();

            return services;
        }

        private static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<LoginViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<MenuViewModel>();
            services.AddTransient<DashboardViewModel>();

            return services;
        }

        private static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddTransient<LoginPage>();
            services.AddTransient<MainPage>();
            services.AddTransient<AppShell>();
            services.AddTransient<DashboardPage>();
            services.AddTransientPopup<SaveTodoPage, SaveTodoViewModel>();

            return services;
        }

        private static IServiceCollection AddNatifServices(this IServiceCollection services)
        {
            services.AddTransient<IStorageService, NatifStorageService>();

            return services;
        }

        public static IConfiguration AddSettingsConfiguration(this ConfigurationManager configuration)
        {
#if DEBUG
            Environment.SetEnvironmentVariable("ENVIRONMENT", "Development");
#else
            Environment.SetEnvironmentVariable("ENVIRONMENT", "Production");
#endif
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("Taskly.Natif.appsettings.json");
            using var stcurrentEnvConfigFile = a.GetManifestResourceStream($"Taskly.Natif.appsettings.{Environment.GetEnvironmentVariable("ENVIRONMENT")}.json");
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .AddJsonStream(stcurrentEnvConfigFile)
                .Build();
            configuration.AddConfiguration(config);
            return configuration;
        }
    }
}
