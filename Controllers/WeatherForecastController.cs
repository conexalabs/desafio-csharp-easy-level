using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_csharp_easy_level.Models;
using desafio_csharp_easy_level.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace desafio_csharp_easy_level.Controllers
{
    [ApiController]
    [Route("api/current")]
    public class WeatherForecastController : ControllerBase
    {
        private WeatherContext _context;
        private OpenWeatherMap _owm;

        public WeatherForecastController(WeatherContext context)
        {
            _context = context;
            _owm = new OpenWeatherMap();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<WeatherForecast>> TodayWeatherFromCityName(string name)
        {
            WeatherForecast currentForecast = await _owm.GetWeatherByName(name);

            _context.WeatherForecasts.Add(currentForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(TodayWeatherFromCityName), currentForecast);
        }

        [HttpGet("{lat},{lon}")]
        public async Task<ActionResult<WeatherForecast>> TodayWeatherFromCityCoordinate(double lat, double lon)
        {
            WeatherForecast currentForecast = await _owm.GetWeatherByCoords(lat, lon);

            _context.WeatherForecasts.Add(currentForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(TodayWeatherFromCityCoordinate), currentForecast);
        }
    }

    [ApiController]
    [Route("api/historic")]
    public class WeatherForecastHistoricController : ControllerBase
    {
        private WeatherContext _context;
        private OpenWeatherMap _owm;

        public WeatherForecastHistoricController(WeatherContext context)
        {
            _context = context;
            _owm = new OpenWeatherMap();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> HistoricWeatherFromCityName(string name)
        {
            string _name = await _owm.GetCityNameByName(name);
            List<WeatherForecast> historicList = _context.WeatherForecasts.Where(f => f.Name == _name).ToList();
            return historicList;
        }

        [HttpGet("{lat},{lon}")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> HistoricWeatherFromCityCoordinate(double lat, double lon)
        {
            // get city name from OWM
            string _name = await _owm.GetCityNameByCoords(lat, lon);

            List<WeatherForecast> historicList = _context.WeatherForecasts.Where(f => f.Name == _name).ToList();
            return historicList;
        }
    }
}
