using Microsoft.Extensions.Hosting;
using SU.Backend.Configuration;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) => { services.AddBackendServices(context.Configuration); })
            .Build();
    }
}