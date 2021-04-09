using OreLavorateStdAPIClient;
using System;
using Xunit;

namespace OreLavorateTests.ApiTests
{
    public class ApiClientTests
    {
        private readonly IServiceProvider Services;
        private readonly ApiClient _client;
        public ApiClientTests()
        {
            var conf = new ConfigureTestServices();
            this.Services = conf.Services;

            this._client = new ApiClient("https://localhost:5001");
        }

        [Fact]
        public void Test_AllUtente()
        {
            var utenti = _client.UtentiGetAsync().Result;
            Assert.True(utenti.Count > 0, "utenti non trovati");
        }

        [Fact]
        public void Test_AllCommessa()
        {
            var commesse = _client.CommesseGetAsync().Result;
            Assert.True(commesse.Count > 0, "commesse non trovate");
        }

        [Fact]
        public void Test_AllOreLavorate()
        {
            var ore = _client.OreLavorateGetAsync().Result;
            Assert.True(ore.Count > 0, "ore lavorate non trovate");
        }
    }
}
