using AppLifetime.Example;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static async Task Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        var services = new ServiceCollection();

        builder.Services.AddHostedService<ExampleHostedService>();
        using IHost host = builder.Build();

        await host.RunAsync();
    }
}