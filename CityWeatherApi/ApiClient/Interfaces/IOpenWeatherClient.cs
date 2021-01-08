using CityWeatherApi.ApiClient.Models;
using CityWeatherApi.ApiClient.Models.Historic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.ApiClient.Interfaces
{
    public interface IOpenWeatherClient
    {
        Task<OpenWeatherModel> GetTempByCityName(string cityName);
        Task<OpenWeatherModel> GetTempByLatLon(float lat, float lon);
        Task<HistoricWeatherModel> GetHistoric(float lat, float lon);
    }
}
