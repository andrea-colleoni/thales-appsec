using Microsoft.Extensions.DependencyInjection;
using OreLavorateLib.Services;
using System;
using Xunit;

namespace OreLavorateTests.LibTests
{
    public class ServiceTests
    {
        private readonly IServiceProvider Services;
        public ServiceTests()
        {
            var conf = new ConfigureTestServices();
            this.Services = conf.Services;
        }

        [Fact]
        public void Test_Utente_Login()
        {
            var srv = Services.GetRequiredService<IUtenteService>();
            Assert.True(srv.Login("test", "test"), "login test failed");
        }
    }
}
