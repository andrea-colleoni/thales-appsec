using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OreLavorateLib.Context;
using OreLavorateLib.Services;

namespace OreLavorateLib
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOreLavorateLibServices(
            this IServiceCollection services, 
            IConfiguration Configuration)
        {
            services.AddDbContext<OrelavorateContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString("OreLavorateDb"))
            );

            services.AddTransient<ICommessaService, CommessaService>();
            services.AddTransient<IOreLavorateService, OreLavorateService>();
            services.AddTransient<IUtenteService, UtenteService>();

            return services;
        }
    }
}
