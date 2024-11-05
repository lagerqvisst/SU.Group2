using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SU.Backend.Configuration;
using SU.Frontend.Views;

namespace SU.Frontend;

public partial class App : Application
{
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddBackendServices(context.Configuration);
                services.AddFrontendServices();
            })
            .Build();
    }

    public static IHost? AppHost { get; private set; }

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