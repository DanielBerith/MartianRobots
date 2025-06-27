using MartianRobots.Application;
using MartianRobots.ConsoleApp;
using MartianRobots.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Setup DI container
        var serviceProvider = ConfigureServices();

        // Run the application
        var app = serviceProvider.GetRequiredService<Application>();
        app.Run();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Register all services using extension methods
        services.AddApplicationServices();
        services.AddInfrastructureServices();

        // Register the main application
        services.AddTransient<Application>();

        return services.BuildServiceProvider();
    }
}