using CityWeatherApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.Services.IServices
{
    public interface ICityWeatherService
    {
        Task<CityWeatherModel> GetTemp(string cityName);
        Task<CityWeatherModel> GetTemp(float lat, float lon);
        Task<List<CityWeatherModel>> GetHistoric( float lat, float lon);
    }
}
