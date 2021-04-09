using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OreLavorateLib.Context
{
    public class DbInitializer
    {
        public static void CreateDatabaseIfNotExists(IHost host)
        {
            var log = host.Services.GetRequiredService<ILogger<DbInitializer>>();
            log.LogInformation("initializing db");
        }
    }
}
