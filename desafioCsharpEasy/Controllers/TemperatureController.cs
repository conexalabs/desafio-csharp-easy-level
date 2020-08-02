using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [Route("actual")]
        [Produces("application/json")]
        public async Task<ActionResult<Weather>> GetActual(
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

        [HttpGet]
        [Route("historic")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Weather>>> GetHistoric(
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