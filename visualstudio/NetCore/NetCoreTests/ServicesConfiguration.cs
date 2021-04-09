using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreLibrary;
using NetCoreLibrary.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTests
{
    public class ServicesConfiguration
    {
        public IServiceProvider Services { get; private set; }

        public ServicesConfiguration()
        {
            var host = Host.CreateDefaultBuilder(null)
                // appsettings.json viene caricato dal defaulthostbuilder automaticamente, se c'è
                //.ConfigureHostConfiguration(config =>
                //    config.AddJsonFile("appsettings.json", false, false)
                //)
                .ConfigureServices((hostConfiguration, services) => {
                    services.AddNetCoreLibraryServices(hostConfiguration.Configuration);
                })
                .Build();
            Services = host.Services;

            DbInitializer.CreateDatabaseIfNotExists(host);
        }

        
    }
}
