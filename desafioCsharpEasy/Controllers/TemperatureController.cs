using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using desafioCsharpEasy.Models;
using desafioCsharpEasy.Processes;

namespace desafioCsharpEasy.Controllers
{
    [ApiController]
    [Route("api/temperature")]
    public class TemperatureController : ControllerBase
    {
        private readonly TemperatureContext _context;
        private readonly TemperatureProcess _process;

        public TemperatureController(TemperatureContext context)
        {
            _context = context; // Apagar em breve
            _process = new TemperatureProcess(_context);
        }

        /// <summary>
        /// Obtains the current temperature in a given city or coordinates
        /// And register it for later checks
        /// </summary>
        /// <remarks>
        /// To query a city by its name use the parameter `city`
        /// To query by its coordinates use the parameters `latitude` and `longitude`
        ///
        /// Samples:
        ///
        ///     GET /api/temperature/current?city=Goiania
        ///
        ///     GET /api/temperature/current?latitude=-16.68&amp;longitude=-49.25
        /// </remarks>
        /// <returns>Object weather with current temperature</returns>
        /// <response code="200">Returns the weather object</response>
        /// <response code="404">If is not possible to deduce the location based on the given parameters</response>
        /// <response code="400">If invalid request</response>
        [HttpGet]
        [Route("current")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Weather>> GetCurrent(
            [FromQuery] string city,
            [FromQuery] double? latitude,
            [FromQuery] double? longitude
        )
        {
            if (!String.IsNullOrEmpty(city))
            {
                var weather = await _process.GetActual(city);
                if (weather == null) return NotFound();
                return weather;
            }
            else if (latitude.HasValue && longitude.HasValue)
            {
                var weather = await _process.GetActual(latitude.Value, longitude.Value);
                if (weather == null) return NotFound();
                return weather;
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtains the history of temperatures in a given city or coordinates
        /// in the last month. It depends on be registered earlier.
        /// </summary>
        /// <remarks>
        /// To query a city by its name use the parameter `city`
        /// To query by its coordinates use the parameters `latitude` and `longitude`
        ///
        /// Samples:
        ///
        ///     GET /api/temperature/current?city=Goiania
        ///
        ///     GET /api/temperature/current?latitude=-16.68&amp;longitude=-49.25
        /// </remarks>
        /// <returns>List of weather objects with temperatures registered in the last month</returns>
        /// <response code="200">Returns the weather list</response>
        /// <response code="404">If is not possible to deduce the location based on the given parameters</response>
        /// <response code="400">If invalid request</response>
        [HttpGet]
        [Route("history")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Weather>>> GetHistory(
            [FromQuery] string city,
            [FromQuery] double? latitude,
            [FromQuery] double? longitude
        )
        {
            if (!String.IsNullOrEmpty(city))
            {
                var historic = await _process.GetHistoric(city);
                if (historic == null) return NotFound();
                return historic.ToList();
            }
            else if (latitude != null && longitude != null)
            {
                var historic = await _process.GetHistoric(latitude.Value, longitude.Value);
                if (historic == null) return NotFound();
                return historic.ToList();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}