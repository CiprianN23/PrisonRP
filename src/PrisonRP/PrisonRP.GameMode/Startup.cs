using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrisonRP.Data;
using PrisonRP.GameMode.Services;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using Serilog;

namespace PrisonRP.GameMode;

public class Startup : IStartup
{
    public void Configure(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        var mariadbVersion = new MariaDbServerVersion(new Version(10, 6, 5));

        services
            .AddSingleton(configuration)
            .AddDbContextPool<ApplicationContext>(options => options.UseMySql(configuration.GetConnectionString("Default"), mariadbVersion))
            .AddSystemsInAssembly()
            .AddTransient<IChatService, ChatService>();

        services.AddLogging(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Debug);
            builder.AddSerilog(logger, true);
        });

    }

    public void Configure(IEcsBuilder builder)
    {
        builder
            .EnableSampEvents() // Enable all stock SA-MP callbacks as events which can be listened to by systems.
            .EnablePlayerCommands(); // Enable player commands being loaded in systems.
    }
}
