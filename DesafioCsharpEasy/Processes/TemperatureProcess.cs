using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesafioCsharpEasy.Models;
using DesafioCsharpEasy.Services;

namespace DesafioCsharpEasy.Processes
{
    class TemperatureProcess
    {
        private readonly TemperatureContext _context;
        private readonly OpenWeatherMapService _service;

        public TemperatureProcess(TemperatureContext context)
        {
            _context = context;
            _service = new OpenWeatherMapService();
        }

        public async Task<Weather> GetActual(string city) =>
            await UpdateWeather(await _service.GetWeatherByCityName(city));

        public async Task<Weather> GetActual(double latitude, double longitude) =>
            await UpdateWeather(await _service.GetWeatherByCoordinates(latitude, longitude));

        public async Task<IEnumerable<Weather>> GetHistoric(string city)
        {
            var actualWeather = await UpdateWeather(await _service.GetWeatherByCityName(city));
            if (actualWeather == null) return null;
            var weathers = _context.Weathers
                .Where(weather => weather.CityId == actualWeather.CityId
                    && weather.Date.Month == DateTime.Now.Month)
                .OrderBy(weather => weather.Date)
                .ToList();
            
            weathers.ForEach(weather => weather.City = actualWeather.City);

            return weathers;
        }

        public async Task<IEnumerable<Weather>> GetHistoric(double latitude, double longitude)
        {
            var actualWeather = await UpdateWeather(await _service.GetWeatherByCoordinates(latitude, longitude));
            if (actualWeather == null) return null;
            var weathers = _context.Weathers
                .Where(weather => weather.CityId == actualWeather.CityId
                    && weather.Date.Month == DateTime.Now.Month)
                .OrderBy(weather => weather.Date)
                .ToList();

            weathers.ForEach(weather => weather.City = actualWeather.City);

            return weathers;
        }

        private async Task<Weather> UpdateWeather(WeatherResponse weatherResponse)
        {
            if (weatherResponse == null) return null;

            var actualWeather = await _context.Weathers
                .FirstOrDefaultAsync(weather => weather.CityId == weatherResponse.id
                    && weather.Date == DateTime.Now.Date);

            var weather = new Weather
            {
                Id = actualWeather?.Id ?? 0,
                Date = DateTime.Now.Date,
                CityId = weatherResponse.id,
                City = new City
                {
                    Name = weatherResponse.name,
                    Latitude = weatherResponse.coord.lat,
                    Longitude = weatherResponse.coord.lon,
                },
                Temperature = weatherResponse.main.temp
            };

            if (actualWeather != null)
            {
                actualWeather.Temperature = weather.Temperature;
                _context.Weathers.Update(actualWeather);
            }
            else
            {
                _context.Weathers.Add(weather);
            }

            await _context.SaveChangesAsync();

            return weather;
        }
    }
}