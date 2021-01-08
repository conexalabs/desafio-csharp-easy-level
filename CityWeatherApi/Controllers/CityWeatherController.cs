using CityWeatherApi.Model;
using CityWeatherApi.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityWeatherController : ControllerBase
    {

        private readonly ILogger<CityWeatherController> _logger;
        private readonly ICityWeatherService _service;
        public CityWeatherController(ILogger<CityWeatherController> logger, ICityWeatherService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// EndPoint resposável pela requisição de temperatura através do nome da cidade
        /// </summary>
        /// <param name="cityName">Nome da cidade</param>
        /// <returns>Nome da cidade e temperatura</returns>
        [HttpGet("{cityName}")]
        public async Task<CityWeatherModel> Get(string cityName)
        {
            return await _service.GetTemp(cityName);
        }
        
        /// <summary>
        /// EndPoint responsável pela requisição de temperatura através da latitude e longitude
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        /// <returns>Nome da cidade e temperatura</returns>
        [HttpGet("{lat}/{lon}")]
        public async Task<CityWeatherModel> Get(float lat, float lon)
        {
            return await _service.GetTemp(lat, lon);
        }

        /// <summary>
        /// EndPoint responsável pela requisição de temperaturas futuras através de latitude e longitude
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        /// <returns>Lista contendo temperatura e data</returns>
        [HttpGet(nameof(Historic))]
        public async Task<List<CityWeatherModel>> Historic(float lat, float lon)
        {
            return await _service.GetHistoric(lat, lon);
        }
    }
}
