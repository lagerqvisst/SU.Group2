using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SU.Backend.Configuration; // Importera din backend DI-konfiguration
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using SU.Frontend.Helper.User;
using SU.Frontend.ViewModels;
using SU.Frontend.Views;
using System;
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
                    services.AddBackendServices();
                    services.AddFrontendServices();


                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();


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
