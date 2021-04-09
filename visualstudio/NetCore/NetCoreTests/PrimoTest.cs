using Microsoft.Extensions.DependencyInjection;
using NetCoreLibrary;
using NetCoreLibrary.Context;
using NetCoreLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NetCoreTests
{
    public class PrimoTest
    {
        private IServiceProvider serviceProvider;

        public PrimoTest()
        {
            var ds = new ServicesConfiguration();
            serviceProvider = ds.Services;
        }

        [Fact]
        public void Test_1()
        {
            var num = new Random().Next(1, 10);
            if (num % 2 == 0)
            {
                Assert.True(num < 10);
            }
            else
            {
                // fail!!
                Assert.True(false, $"il numero casuale è {num}");
            }
        }

        [Fact]
        public void Test_Context()
        {
            // var ctx = new PersoneContext();
            var serv = serviceProvider.GetService<IPersonaService>();
            var persona = serv.PersonaDiTest();
            Assert.True(true, "qualcosa è andato storto");
            Assert.NotNull(persona);
        }
    }
}
