using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Interfaces.ExternaWeatherMaps;
using Application.ViewModels.City;
using Application.ViewModels.City.BadRequest;
using Application.ViewModels.City.Response;
using AutoMapper;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infra.Data.APIExterna
{
    public class ApiExternalWeatherMaps : IApiExternalWeatherMaps
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        public ApiExternalWeatherMaps(IHttpClientFactory clientFactory, IMapper mapper)
        {
            _clientFactory = clientFactory;
            _mapper = mapper;
        }

        public async Task<CityViewModelResponse> GetTempByCity(string cityName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://api.openweathermap.org/data/2.5/weather?q="+cityName+"&units=metric&lang=pt_br&appid=142d374d2ea6105f400f36546592a3d4");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            Stream responseStream;
            if (!response.IsSuccessStatusCode)
            {
                responseStream = await response.Content.ReadAsStreamAsync();
                var objBadRequest = await JsonSerializer.DeserializeAsync
                    <BadRequestViewModelAPI>(responseStream);
                throw new Exception(objBadRequest.message);
            }
            responseStream = await response.Content.ReadAsStreamAsync();
            var objApi = await JsonSerializer.DeserializeAsync
                <CityAPI>(responseStream);
            return _mapper.Map<CityViewModelResponse>(objApi);
        }

        public async Task<CityViewModelResponse> GetTempByLonLat(string lat, string lon)
        {
            var teste = new CityViewModelResponse();
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://api.openweathermap.org/data/2.5/weather?lat="+lat+"&lon="+lon+"&units=metric&appid=142d374d2ea6105f400f36546592a3d4");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            Stream responseStream;
            JsonSerializerOptions options;
            if (!response.IsSuccessStatusCode)
            {
                responseStream = await response.Content.ReadAsStreamAsync();
                options = new JsonSerializerOptions
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                var objBadRequest = await JsonSerializer.DeserializeAsync
                    <BadRequestViewModelAPI>(responseStream, options);
                throw new Exception(objBadRequest.message);
                
            }
            responseStream = await response.Content.ReadAsStreamAsync();
            options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var objApi = await JsonSerializer.DeserializeAsync
                <CityAPI>(responseStream, options);
            teste = _mapper.Map<CityViewModelResponse>(objApi);
            return teste;
        }
        
    }
}