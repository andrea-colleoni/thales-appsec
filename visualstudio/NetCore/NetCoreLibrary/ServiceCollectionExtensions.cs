using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreLibrary.Context;
using NetCoreLibrary.Services;

namespace NetCoreLibrary
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNetCoreLibraryServices(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddDbContext<PersoneContext>(o =>
               o.UseSqlServer(configuration.GetConnectionString("PersoneDb"))
           );

            services.AddTransient<IPersonaService, PersonaService>();

            return services;
        }
    }
}
