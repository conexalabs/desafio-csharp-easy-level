using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_csharp_easy_level.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace desafio_csharp_easy_level.Controllers
{
    [ApiController]
    [Route("api/current")]
    public class WeatherForecastController : ControllerBase
    {
        private WeatherContext _context;
        public WeatherForecastController(WeatherContext context)
        {
            _context = context;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<WeatherForecast>> TodayWeatherFromCityName(string name)
        {
            double _temp = 12.5;
            DateTime _date = DateTime.Now;
            WeatherForecast currentForecast = new WeatherForecast { Name = name, Temp = _temp, Date = _date };

            _context.WeatherForecasts.Add(currentForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(TodayWeatherFromCityName), currentForecast);
        }

        [HttpGet("{lat},{lon}")]
        public async Task<ActionResult<WeatherForecast>> TodayWeatherFromCityCoordinate(double lat, double lon)
        {
            double _temp = 12.5;
            DateTime _date = DateTime.Now;
            WeatherForecast currentForecast = new WeatherForecast { Name = "Cidadelândia", Temp = _temp, Date = _date };

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
        public WeatherForecastHistoricController(WeatherContext context)
        {
            _context = context;
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<WeatherForecast>> HistoricWeatherFromCityName(string name)
        {
            List<WeatherForecast> historicList = _context.WeatherForecasts.Where(f => f.Name == name).ToList();
            return historicList;
        }

        [HttpGet("{lat},{lon}")]
        public ActionResult<IEnumerable<WeatherForecast>> HistoricWeatherFromCityCoordinate(double lat, double lon)
        {
            // get city name from OWM
            string name = "goiania";

            List<WeatherForecast> historicList = _context.WeatherForecasts.Where(f => f.Name == name).ToList();
            return historicList;
        }
    }
}
