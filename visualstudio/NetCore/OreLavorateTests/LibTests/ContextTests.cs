using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OreLavorateLib.Context;
using System;
using System.Linq;
using Xunit;

namespace OreLavorateTests.LibTests
{
    public class ContextTests
    {
        private readonly IServiceProvider Services;
        public ContextTests()
        {
            var conf = new ConfigureTestServices();
            this.Services = conf.Services;
        }

        [Fact]
        public void Test_Context()
        {
            using (var ctx = Services.GetRequiredService<OrelavorateContext>())
            {
                Assert.True(ctx.Database.CanConnect(), "db can't connect");
            }
        }
        [Fact]
        public void Test_FirstOrDefault()
        {
            using (var ctx = Services.GetRequiredService<OrelavorateContext>())
            {
                var utente = ctx.Utentes
                    .Include(u => u.OreLavorates)
                    .AsNoTracking() // predispone una conx in sola lettura (no tx)
                    .FirstOrDefault(u => u.Username == "andrea");

                Assert.NotNull(utente);
                Assert.True(utente.OreLavorates.Count == 0);
            }
        }
        [Fact]
        public void Test_Find()
        {
            using (var ctx = Services.GetRequiredService<OrelavorateContext>())
            {
                var utente = ctx.Utentes.Find("andrea");

                Assert.NotNull(utente);
                Assert.True(utente.OreLavorates.Count == 0);
            }
        }
    }
}
