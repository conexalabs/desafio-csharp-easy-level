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
    [Route("api/forecast")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet("{name}")]
        public ActionResult<WeatherForecast> TodayWeatherFromCityName(string name)
        {
            return CreatedAtAction("TodayWeatherFromCityName", new WeatherForecast { Name = name, Temp = 12.5, Date = "02/01/2021" });
        }

        [HttpGet("{lat},{lon}")]
        public ActionResult<WeatherForecast> TodayWeatherFromCityCoordinate(double lat, double lon)
        {
            return CreatedAtAction("TodayWeatherFromCityCoordinate", new WeatherForecast { Name = "Alguma cidade", Temp = 21.5, Date = "02/01/2021" });
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<WeatherForecast>> HistoricWeatherFromCityName(string name)
        {
            return new List<WeatherForecast>{
                new WeatherForecast { Name = "Alguma cidade", Temp = 21.5, Date = "02/01/2021" },
                new WeatherForecast { Name = "Outra cidade", Temp = 21.5, Date = "02/01/2021" },
                new WeatherForecast { Name = "Uma cidade", Temp = 21.5, Date = "02/01/2021" }
            };
        }

        [HttpGet("{lat},{lon}")]
        public ActionResult<IEnumerable<WeatherForecast>> HistoricWeatherFromCityCoordinate(double lat, double lon)
        {
            return new List<WeatherForecast>{
                new WeatherForecast { Name = "Alguma cidade", Temp = 21.5, Date = "02/01/2021" },
                new WeatherForecast { Name = "Outra cidade", Temp = 21.5, Date = "02/01/2021" },
                new WeatherForecast { Name = "Uma cidade", Temp = 21.5, Date = "02/01/2021" }
            };
        }

    }
}
