using Microsoft.AspNetCore;

namespace PizzaStore.Models;

public class WebServer
{
    public static void Init()
    {
        IWebHostBuilder builder = CreateWebHostBuilder(null);
        IWebHost host = builder.Build();
        host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();

        return WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://*:5000")
            .UseConfiguration(config)
            .UseStartup<Startup>();
    }
}