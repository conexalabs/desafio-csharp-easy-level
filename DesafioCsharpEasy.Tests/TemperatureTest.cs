using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using DesafioCsharpEasy;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DesafioCsharpEasy.Tests
{
    public class TemperatureTest
    {
        private readonly HttpClient _client;

        public TemperatureTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [Fact]
        public async Task GetCurrentTemperatureByCityName()
        {
            var uri = $"/api/temperature/current?city=Goiania";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            dynamic obj = JObject.Parse(json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(obj?.temperature);
        }

        [Fact]
        public async Task GetCurrentTemperatureByInexistentCityName()
        {
            var uri = $"/api/temperature/current?city=Pasargada";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetCurrentTemperatureByCoordinates()
        {
            var uri = $"/api/temperature/current?latitude=-16.68&longitude=-49.25";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            dynamic obj = JObject.Parse(json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(obj?.temperature);
        }

        [Fact]
        public async Task GetCurrentTemperatureByInexistentCoordinates()
        {
            var uri = $"/api/temperature/current?latitude=100&longitude=100";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetCurrentTemperatureByInvalidCoordinates()
        {
            var uri = $"/api/temperature/current?latitude=abcd&longitude=dcba";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetCurrentTemperatureWithoutParameters()
        {
            var uri = $"/api/temperature/current";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetTemperatureHistoryByCityName()
        {
            var uri = $"/api/temperature/history?city=Goiania";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            dynamic array = JArray.Parse(json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(array[0]?.temperature);
        }

        [Fact]
        public async Task GetTemperatureHistoryByInexistentCityName()
        {
            var uri = $"/api/temperature/history?city=Pasargada";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetTemperatureHistoryByCoordinates()
        {
            var uri = $"/api/temperature/history?latitude=-16.68&longitude=-49.25";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            dynamic array = JArray.Parse(json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(array[0]?.temperature);
        }

        [Fact]
        public async Task GetTemperatureHistoryByInexistentCoordinates()
        {
            var uri = $"/api/temperature/history?latitude=100&longitude=100";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetTemperatureHistoryByInvalidCoordinates()
        {
            var uri = $"/api/temperature/history?latitude=abcd&longitude=dcba";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetTemperatureHistoryWithoutParameters()
        {
            var uri = $"/api/temperature/history";
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
