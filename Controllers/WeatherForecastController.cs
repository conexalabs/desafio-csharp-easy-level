using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_csharp_easy_level.Models;
using desafio_csharp_easy_level.Services;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary> Retorna a Temperatura atual da cidade requisitada. </summary>
        /// <remarks> 
        ///     Realiza uma requizição à API do Open Weather Map para obter a
        ///     temperatura atual da cidade requisitada.
        ///     O resultado é então gravado no banco de dados e retornado para
        ///     o requisitante.
        /// </remarks>
        /// <example>
        ///     GET /api/current/goiania
        /// </example>
        /// <param name="name">Nome da cidade buscada.</param>
        [HttpGet("{name}")]
        public async Task<ActionResult<WeatherForecast>> TodayWeatherFromCityName(string name)
        {
            WeatherForecast currentForecast = await _owm.GetWeatherByName(name);

            _context.WeatherForecasts.Add(currentForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(TodayWeatherFromCityName), currentForecast);
        }

        /// <summary> 
        ///     Retorna a Temperatura atual da cidade mais próxima das coordenadas
        ///     passadas.
        /// </summary>
        /// <remarks> 
        ///     Realiza uma requizição à API do Open Weather Map para obter a
        ///     temperatura atual da cidade mais próxima das coordenadas.
        ///     O resultado é então gravado no banco de dados e retornado para
        ///     o requisitante.
        /// </remarks>
        /// <example>
        ///     GET /api/current/-16.6,-49.2
        /// </example>
        /// <param name="lat">Latitude da coordenada buscada.</param>
        /// <param name="lon">Longitude da coordenada buscada.</param>
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
    public class WeatherForecastHistoricController: ControllerBase{
        
        private WeatherContext _context;
        private OpenWeatherMap _owm;

        public WeatherForecastHistoricController(WeatherContext context)
        {
            _context = context;
            _owm = new OpenWeatherMap();
        }

        /// <summary> 
        ///     Retorna uma lista com as Temperatura que foram gravadas
        ///     anteriormente.
        /// </summary>
        /// <remarks> 
        ///     Encontra todos os registros de temperaturas de uma determinada
        ///     cidade. 
        ///     Realiza uma chamada a API do Open Weather Map para corrigir o 
        ///     nome da cidade buscada.
        /// </remarks>
        /// <example>
        ///     GET /api/historic/goiania
        /// </example>
        /// <param name="name">Nome da cidade buscada.</param>
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> HistoricWeatherFromCityName(string name)
        {
            string _name = await _owm.GetCityNameByName(name);
            List<WeatherForecast> historicList = _context.WeatherForecasts.Where(f => f.Name == _name).ToList();
            return Ok(historicList);
        }

        /// <summary> 
        ///     Retorna uma lista com as Temperatura que foram gravadas
        ///     anteriormente. 
        /// </summary>
        /// <remarks> 
        ///     Encontra todos os registros de temperaturas de uma determinada
        ///     cidade.
        ///     Realiza uma chamada a API do Open Weather Map para obter o 
        ///     nome da cidade mais próxima das coordenas passadas.
        /// </remarks>
        /// <example>
        ///     GET /api/historic/-16.6,-49.2
        /// </example>
        /// <param name="lat">Latitude da coordenada buscada.</param>
        /// <param name="lon">Longitude da coordenada buscada.</param>

        [HttpGet("{lat},{lon}")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> HistoricWeatherFromCityCoordinate(double lat, double lon)
        {
            // get city name from OWM
            string _name = await _owm.GetCityNameByCoords(lat, lon);

            List<WeatherForecast> historicList = _context.WeatherForecasts.Where(f => f.Name == _name).ToList();
            return Ok(historicList);
        }
    }
}
