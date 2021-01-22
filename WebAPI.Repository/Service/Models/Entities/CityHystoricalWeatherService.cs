using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebAPI.Data.Interfaces;
using WebAPI.Data.Interfaces.Service.Models.Entities;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Data.Service.Models.Entities
{
    public class CityHystoricalWeatherService : ICityHystoricalWeatherService
    {
        public readonly IUnitOfWork _unity;

        public CityHystoricalWeatherService(IUnitOfWork unit)
        {
            _unity = unit;
        }
        public IEnumerable<CityWeather> GetWeathersFromMonth(string cityName)
        {

            Expression<Func<CityWeather, bool>> expression = e => e.Date >= GetFirstDayOfMonth() && e.Date <= GetLastDayOfMonth() && e.CityName.Equals(cityName);
            return _unity.CityWeatherRepository.GetAll(expression);
        }

        public IEnumerable<CityWeather> GetWeathersFromMonth(double latitude, double longitude)
        {
            Expression<Func<CityWeather, bool>> expression = e => e.Date >= GetFirstDayOfMonth() && e.Date <= GetLastDayOfMonth() && e.Latitude == latitude && e.Longitude == longitude;
            return _unity.CityWeatherRepository.GetAll(expression);
        }

        private DateTime GetFirstDayOfMonth()
        {
            var actualDate = DateTime.UtcNow;
            var firstDayOfMonth = new DateTime(actualDate.Year, actualDate.Month, 1);
            return firstDayOfMonth;
        }

        private DateTime GetLastDayOfMonth()
        {
            var actualDate = DateTime.UtcNow;
            var firstDayOfMonth = new DateTime(actualDate.Year, actualDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(0000);
            return lastDayOfMonth;
        }
    }
}
