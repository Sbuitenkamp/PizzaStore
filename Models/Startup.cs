namespace PizzaStore.Models;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync(GetPage("index"));
            });
        });
    }

    private string GetPage(string pageName)
    {
        return System.IO.File.ReadAllText($"./Client/{pageName.ToLower()}.html");
    }
}