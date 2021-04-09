using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OreLavorateLib;
using OreLavorateLib.Context;
using OreLavorateStdAPIClient;
using System;

namespace OreLavorateTests
{
    class ConfigureTestServices
    {
        public IServiceProvider Services { get; private set; }

        public ConfigureTestServices()
        {
            var host = Host.CreateDefaultBuilder(null)
                .ConfigureHostConfiguration(config =>
                    config.AddJsonFile("appsettings.json", false, false)
                )
                .ConfigureServices((hostConfiguration, services) => {
                    services.AddOreLavorateLibServices(hostConfiguration.Configuration);
                })
                .Build();
            Services = host.Services;

            DbInitializer.CreateDatabaseIfNotExists(host);
        }
    }
}
