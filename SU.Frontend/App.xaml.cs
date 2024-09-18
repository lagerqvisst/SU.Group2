﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SU.Backend.Configuration; // Importera din backend DI-konfiguration
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using SU.Frontend.Helper.User;
using SU.Frontend.ViewModels;
using SU.Frontend.ViewModels.UserControls;
using SU.Frontend.Views;
using SU.Frontend.Views.UserControls;
using System.Windows;

namespace SU.Frontend
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Registrera din backend DI-konfiguration
                    services.AddApplicationServices();

                    // Registrera både LoginWindow och LoginViewModel
                    services.AddTransient<TaskbarViewModel>();
                    services.AddTransient<TaskbarView>();
                    services.AddTransient<LoginViewModel>();
                    services.AddTransient<LoginWindow>();

                    //Frontend services
                    services.AddScoped<INavigationService, NavigationService>(); 
                    services.AddSingleton<ILoggedInUserService, LoggedInUserService>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();


            // Hämta LoginViewModel och sätt DataContext
            var loginViewModel = AppHost.Services.GetRequiredService<LoginViewModel>();
            var startupWindow = AppHost.Services.GetRequiredService<LoginWindow>();
            startupWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (AppHost)
            {
                await AppHost!.StopAsync();
            }

            base.OnExit(e);
        }
    }
}
