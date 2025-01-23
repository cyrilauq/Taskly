﻿using Microsoft.Extensions.Configuration;
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

            return services;
        }

        private static IServiceCollection AddNatifServices(this IServiceCollection services)
        {
            services.AddTransient<IStorageService, NatifStorageService>();

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
