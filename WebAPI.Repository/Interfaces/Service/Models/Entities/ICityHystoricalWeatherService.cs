using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Data.Interfaces.Service.Models.Entities
{
    public interface ICityHystoricalWeatherService
    {
        public IEnumerable<CityWeather> GetWeathersFromMonth(string cityName);
        public IEnumerable<CityWeather> GetWeathersFromMonth(double latitude, double longitude);
    }
}
