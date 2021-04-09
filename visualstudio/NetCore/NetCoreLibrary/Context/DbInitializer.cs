using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreLibrary.Model;
using System;

namespace NetCoreLibrary.Context
{
    public class DbInitializer
    {
        public static void CreateDatabaseIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var ctx = services.GetRequiredService<PersoneContext>();
                    // ctx.Database.EnsureCreated();
                    DbInitializer.Inialize(ctx);
                }
                catch (Exception ex)
                {
                    var log = services.GetRequiredService<ILogger<DbInitializer>>();
                    log.LogError(ex, "errore creando il DB");
                }
            }
        }

        public static void Inialize(PersoneContext ctx)
        {
            ctx.Database.EnsureCreated();
            // Cosa fa EnsureCreated?
            // - elmina il db se c'è
            // - applica le migrations (si porta all'ultima versione del modello)
            // - crea db e schema

            var personeIniziali = new Persona[]
            {
                new Persona{Email="mario@rossi.it", Nome="Mario", Cognome="Rossi"}
            };

            ctx.Persone.AddRange(personeIniziali);
            ctx.SaveChanges();
        }
    }
}
