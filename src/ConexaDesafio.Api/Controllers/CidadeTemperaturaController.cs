using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConexaDesafio.Api.ViewModels;
using ConexaDesafio.Dados.Contexto;
using ConexaDesafio.Dados.Repositorio;
using ConexaDesafio.Negocio.Models;
using ConexaDesafio.Negocio.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConexaDesafio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeTemperaturaController : ControllerBase
    {
        #region Serviços e mapeamento
        private readonly CidadeTemperaturaServico _cidadeServico = new CidadeTemperaturaServico();
        private readonly ConexaDesafioDbContext _db;
        #endregion

        public CidadeTemperaturaController(ConexaDesafioDbContext db)
        {
            _db = db;
        }

        [HttpGet("{cidade}")]
        public async Task<IActionResult> Get(string cidade)
        {
            try
            {
                var reposta = await _cidadeServico.MonteResultadoApi(cidade);

                var cidadeTemperaturaVm = ObtenhaCidadeTemperaturaVm(reposta);

                var temperatura = MapeieTemperatura(cidadeTemperaturaVm);

                var cidadeTemperatura = MapeieCidadeTemperatura(cidadeTemperaturaVm);

                _db.Add(cidadeTemperatura);

                return Ok(new { Temperatura = cidadeTemperaturaVm.Main.Temp });
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error: {httpRequestException.Message}");
            }

        }

        [HttpGet("{lat}/{lon}")]
        public async Task<IActionResult> GetLatitudeLongitude(string lat, string lon)
        {
            try
            {
                var reposta = await _cidadeServico.MonteResultadoApiLatLon(lat, lon);

                var cidadeTemperaturaVm = ObtenhaCidadeTemperaturaVm(reposta);

                var temperatura = MapeieTemperatura(cidadeTemperaturaVm);

                var cidadeTemperatura = MapeieCidadeTemperatura(cidadeTemperaturaVm);

                _db.Add(cidadeTemperatura);

                return Ok(new { Temperatura = cidadeTemperaturaVm.Main.Temp });
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error: {httpRequestException.Message}");
            }
        }

        private CidadeTemperaturaVm ObtenhaCidadeTemperaturaVm(string reposta)
        {
            return JsonConvert.DeserializeObject<CidadeTemperaturaVm>(reposta);
        }

        private CidadeTemperatura MapeieCidadeTemperatura(CidadeTemperaturaVm cidadeTemperaturaVm)
        {
            return new CidadeTemperatura()
            {
                Id = cidadeTemperaturaVm.Id,
                Latitude = cidadeTemperaturaVm.Coord.lat,
                Longitude = cidadeTemperaturaVm.Coord.lon,
                Name = cidadeTemperaturaVm.Name,
            };
        }

        private Temperatura MapeieTemperatura(CidadeTemperaturaVm cidadeTemperaturaVm)
        {
            return new Temperatura()
            {
                CidadeId = cidadeTemperaturaVm.Id,
                TemperaturaCelsius = cidadeTemperaturaVm.Name,
                Data = DateTime.Now.Date,
            };
        }
    }
}
