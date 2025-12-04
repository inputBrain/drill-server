using Microsoft.AspNetCore;
using static System.IO.Directory;

namespace Drill.Server.Host;

static internal class Program
{
    public static async Task Main(string[] args)
    {
        var webHost = BuildWebHost(args);
        await InitWebServices(webHost);
    }
        
    private static async Task InitWebServices(IWebHost webHost)
    {
        await webHost.RunAsync();
        await webHost.StopAsync();
        Environment.Exit(0);
    }


    private static IWebHost BuildWebHost(string[] args)
    {
        new ConfigurationBuilder()
            .SetBasePath(GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
    }
}