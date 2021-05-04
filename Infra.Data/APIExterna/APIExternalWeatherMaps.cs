using System;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.ViewModels;
using Application.ViewModels.City;
using Application.ViewModels.City.BadRequest;
using Application.ViewModels.City.Response;
using AutoMapper;
using Newtonsoft.Json;
using Coord = Application.Entidades.City.Coord;
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

        public async Task<CityViewModelResponse> GetTempByCity(string Cidade, string metric)
        {
            CityViewModelResponse teste = new CityViewModelResponse();
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://api.openweathermap.org/data/2.5/weather?q="+Cidade+"&units="+metric+"&lang=pt_br&appid=142d374d2ea6105f400f36546592a3d4");
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
                    WriteIndented = true,
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
            var objAPI = await JsonSerializer.DeserializeAsync
                <CityAPI>(responseStream, options);
            teste = _mapper.Map<CityViewModelResponse>(objAPI);
            return teste;
        }

        public async Task<CityViewModelResponse> GetTempByLonLat(string lat, string lon)
        {
            CityViewModelResponse teste = new CityViewModelResponse();
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

        public Task<CityViewModelResponse> GetTempMouthByCity(string Cidade, string KeyAPI)
        {
            throw new NotImplementedException();
        }

        public Task<CityViewModelResponse> GetTempMouthByLonLat(string Cidade, string KeyAPI)
        {
            throw new NotImplementedException();
        }
    }
}