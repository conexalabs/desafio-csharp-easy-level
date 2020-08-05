using ConexaDesafio.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConexaDesafio.Teste
{
    public class ConexaTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public ConexaTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();

        }

        [Fact]
        public async Task BuscarPorNome()
        {
            var r = await _client.PostAsync($"/api/CidadeTemperatura/Sorocaba", null);
            r.EnsureSuccessStatusCode();
            Assert.Contains("temperatura", await r.Content.ReadAsStringAsync());
        }
        [Fact]
        public async Task BuscarPorLocalizacao()
        {
            var r = await _client.PostAsync($"/api/Weather/lat=-23.50&lon=-47.45", null);

            r.EnsureSuccessStatusCode();
            var resposta = await r.Content.ReadAsStringAsync();
            Assert.Contains("temperatura", resposta);
        }
    }
}
