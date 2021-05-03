using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Entidades.City;
using Application.ViewModels.City;
using Newtonsoft.Json.Linq;

namespace Infra.Data.APIExterna
{
    public class ApiExternalWeatherMaps : IApiExternalWeatherMaps
    {
        
        private readonly IHttpClientFactory _clientFactory;

        public ApiExternalWeatherMaps(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<CityViewModel> GetTempByCity(string Cidade, string metric)
        {
            CityViewModel teste = new CityViewModel();
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://www.api.openweathermap.org/data/2.5/weather?q="+Cidade+"&Units="+metric+"&lang=pt_br&appid=142d374d2ea6105f400f36546592a3d4");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) return teste;
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            teste = await JsonSerializer.DeserializeAsync
                <CityViewModel>(responseStream, options);
            return teste;
        }

        public async Task<CityViewModel> GetTempByLonLat(string lon, string lat)
        {
            CityViewModel teste = new CityViewModel();
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://api.openweathermap.org/data/2.5/weather?lat="+lat+"&lon="+lon+"&units=metric&appid=142d374d2ea6105f400f36546592a3d4");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) return teste;
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            teste = await JsonSerializer.DeserializeAsync
                <CityViewModel>(responseStream, options);
            return teste;
        }

       
    }
}