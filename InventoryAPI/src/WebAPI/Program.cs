using Microsoft.EntityFrameworkCore;
using InventoryAPI.Infrastructure.Persistence;

namespace InventoryAPI.WebAPI;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var environment = services.GetRequiredService<IWebHostEnvironment>();
            if (environment.IsDevelopment())
            {
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    if (context.Database.IsNpgsql())
                    {
                        await context.Database.MigrateAsync();
                    }
                
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogInformation("Successfully migrated database");
                
                    await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database");

                    throw;
                }
            }
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                    })
                    .UseUrls($"http://*:{Environment.GetEnvironmentVariable("PORT")}")
                    .UseStartup<Startup>());
}