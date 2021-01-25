using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrisonRP.Database.Interfaces;
using PrisonRP.Database.Migrations;
using PrisonRP.Database.Repositories;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using Serilog;
using System;

namespace PrisonRP.GameMode
{
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

            // Services can be added to the service collection which can later be accessed by systems and other services.
            services
                .AddSingleton(configuration)
                .AddTransient<IPlayerAccountRepository, PlayerAccountRepository>()
                .AddSystemsInAssembly();

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddSerilog(logger, true);
            });

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddMySql5()
                .WithGlobalConnectionString(configuration.GetConnectionString("Default"))
                .ScanIn(typeof(AddPlayerAccountTable).Assembly).For.Migrations());
        }

        public void Configure(IEcsBuilder builder)
        {
            UpdateDatabase(builder.Services);

            // Enable or disable features of ECS or other libraries here.
            builder
                .EnableSampEvents() // Enable all stock SA-MP callbacks as events which can be listened to by systems.
                .EnablePlayerCommands(); // Enable player commands being loaded in systems.
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}