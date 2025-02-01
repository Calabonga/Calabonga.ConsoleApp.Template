using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace Calabonga.ConsoleApp;

/// <summary>
/// Create Container for Console App
/// </summary>
public static class ConsoleApp
{
    /// <summary>
    /// Creates container <see cref="ServiceCollection"/>
    /// </summary>
    /// <returns></returns>
    public static ServiceProvider CreateContainer()
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: false)
            .AddDotNetEnv(".env", LoadOptions.TraversePath())
            .Build();

        var logger = new LoggerConfiguration().MinimumLevel.Verbose()
            .WriteTo.Console()
            .CreateLogger();

        services.AddLogging(x =>
        {
            x.AddSerilog(logger);
        });

        services.Configure<AppSettings>(x => configuration.GetSection(nameof(AppSettings)).Bind(x));

        return services.BuildServiceProvider();
    }

    /// <summary>
    /// Show some demo how to use 
    /// </summary>
    /// <param name="container"></param>
    public static void ShowDemo(ServiceProvider container)
    {
        var logger = container.GetRequiredService<ILogger<Program>>();
        var appSettingsOptions = container.GetRequiredService<IOptions<AppSettings>>();
        var appSettings = appSettingsOptions.Value;

        logger.LogInformation("Console Application {0} contains:", appSettings.Name);

        foreach (var item in appSettings.TemplateContains!)
        {
            logger.LogInformation("....{0}", item);
        }

        logger.LogInformation("This sample show how to use Configuration pattern with Dependency Injection Container.");
        logger.LogInformation(appSettings.Welcome);

        logger.LogInformation("DEMO_APPLICATION_TYPE: {0}", Environment.GetEnvironmentVariable("DEMO_APPLICATION_TYPE"));
        logger.LogInformation("DEMO_APPLICATION_VERSION: {0}", Environment.GetEnvironmentVariable("DEMO_APPLICATION_VERSION"));
    }
}
