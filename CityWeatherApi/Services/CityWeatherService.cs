using CityWeatherApi.ApiClient.Interfaces;
using CityWeatherApi.Model;
using CityWeatherApi.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.Services
{
    public class CityWeatherService : ICityWeatherService
    {
        private readonly IOpenWeatherClient _openWeatherClient;
        public CityWeatherService(IOpenWeatherClient openWeatherClient)
        {
            _openWeatherClient = openWeatherClient;
        }

        public async Task<List<CityWeatherModel>> GetHistoric(float lat, float lon)
        {
            var response = await _openWeatherClient.GetHistoric(lat, lon);
            var result = new List<CityWeatherModel>();
            foreach (var day in response.daily)
            {
                result.Add(new CityWeatherModel
                {
                    CityTemp = day.temp.day,
                    Date = DateTimeOffset.FromUnixTimeSeconds(day.dt)
                });
            }
            return result;
        }

        public async Task<CityWeatherModel> GetTemp(string cityName)
        {
            var response = await _openWeatherClient.GetTempByCityName(cityName);
            if (response == null || response.cod == 404)//Verificar forma de tratativa para erros 400
                return null;
            var result = new CityWeatherModel
            {
                CityName = response.name,
                CityTemp = response.main.temp
            };

            return result;
        }

        public async Task<CityWeatherModel> GetTemp(float lat, float lon)
        {
            var response = await _openWeatherClient.GetTempByLatLon(lat, lon);
            var result = new CityWeatherModel
            {
                CityName = response.name,
                CityTemp = response.main.temp
            };

            return result;
        }
    }
}
