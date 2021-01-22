using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using WebAPI.Domain.Helpers;
using WebAPI.Domain.Interfaces.Service.OpenWeather;
using WebAPI.Domain.Models.Service.OpenWeather;

namespace WebAPI.Domain.Services.OpenWeather
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly string _apiURL;
        private readonly string _apiKey;
        private readonly string _byCityEndpoint;
        private readonly string _byGeoCoordinatesEndpoint;

        private readonly IConfiguration _configuration;
        
        public OpenWeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["OpenWeatherConfiguration:ApiKey"];
            _apiURL = _configuration["OpenWeatherConfiguration:ApiBaseURL"];
            _byCityEndpoint = _configuration["OpenWeatherConfiguration:TemperatureByCityEndpoint"];
            _byGeoCoordinatesEndpoint = _configuration["OpenWeatherConfiguration:TemperatureByGeoCoordinatesEndpoint"];
        }
        private CurrentModel QueryOpenWeatherEndpoint(string endpointToFormat, params object[] args)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(String.Format(endpointToFormat, args)).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return HttpHelper.ConvertToModel<CurrentModel>(response);
                    }
                    else
                    {
                        return default;
                    }

                }
            }
            catch (Exception)
            {

                return default;
            }
        }

        public CurrentModel GetTemperatureByCityName(string cityName)
        {
            return QueryOpenWeatherEndpoint(_byCityEndpoint, _apiURL, cityName, _apiKey);
        }

        public CurrentModel GetTemperatureByGeoCoordinates(string latitude, string longitude)
        {
            return QueryOpenWeatherEndpoint(_byGeoCoordinatesEndpoint, _apiURL, latitude, longitude, _apiKey);
        }
    }
}
