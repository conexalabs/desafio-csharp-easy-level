using DesafioCsharpEasy.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace DesafioCsharpEasy.Services.OpenWeatherMap
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly string apiBaseUrl;
        private readonly string apiKey;
        private IConfiguration _configuration;

        public OpenWeatherMapService(IConfiguration configuration)
        {
            _configuration = configuration;
            apiKey = _configuration["OpenWeatherMaps:ApiKey"];
            apiBaseUrl = _configuration["OpenWeatherMaps:ApiBaseUrl"];
        }

        public CurrentModel GetTemperatureByCity(string city)
        {
            return GetTemperature(SetUpUrl(GetParamCity(city)));
        }

        public CurrentModel GetTemperatureByCoord(string lat, string lon)
        {
            return GetTemperature(SetUpUrl(GetParamCoord(lat, lon)));
        }

        private CurrentModel GetTemperature(string url)
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = httpClient.GetAsync(url).Result;
                var result = HttpResponse.ConvertToObj<CurrentModel>(response);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string SetUpUrl(string param)
        {
            var url = apiBaseUrl + param;

            url += $"&appid={apiKey}";

            return url;
        }

        private string GetParamCity(string city)
        {
            return $"&q={city}";
        }

        private string GetParamCoord(string lat, string lon)
        {
            return $"&lat={lat}&lon={lon}";
        }
    }
}
