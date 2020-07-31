using desafio_conexa;
using desafio_conexa.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TesteApi
{
    public class TesteApi
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public TesteApi()
        {

            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();

        }
        [Fact]
        public async Task BuscarPorNome()
        {
            var nome = "Goiânia";
            var response = await _client.PostAsync($"/api/Weather/por-nome?nomeCidade={nome}", null);
        
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Contains("temperatura", responseString);
        }
        [Fact]
        public async Task BuscarPorLocalizacao()
        {
            var lat = "-16,28";
            var lon = "-49,45";
            var response = await _client.PostAsync($"/api/Weather/por-localizacao?lat={lat}&lon={lon}", null);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Contains("temperatura", responseString);
        }

        [Fact]
        public async Task BuscarHistoricoPorLocalizacao()
        {
            var lat = "-16,28";
            var lon = "-49,45";
            var response = await _client.GetAsync($"/api/Weather/historico?lat={lat}&lon={lon}");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Contains("temperatura", responseString);
        }

        [Fact]
        public async Task BuscarHistoricoPorNome()
        {
            var nome = "Goiânia";
            var response = await _client.GetAsync($"/api/Weather/historico?nomeCidade={nome}");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Contains("temperatura", responseString);
        }
    }
}
