using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_conexa.DbContexts;
using desafio_conexa.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace desafio_conexa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly CidadesDbContext contextoCidade;
        private readonly TemperaturasDbContext contextoTemperatura;
        public WeatherController(CidadesDbContext contexto, TemperaturasDbContext contextoT)
        {
            contextoCidade = contexto;
            contextoTemperatura = contextoT;
        }
        [HttpPost]
        [Route("por-nome")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Retorno),StatusCodes.Status202Accepted)]
        public IActionResult MostrarTemperaturaPorNome( string nomeCidade)
        {

            var resultado = new CidadeService(contextoCidade, contextoTemperatura).RetornaTemperaturaPorNome(nomeCidade);
            if (resultado.Sucesso)
                return Ok(resultado);
            else
                return BadRequest(resultado);

        }

        [HttpPost]
        [Route("por-localizacao")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status202Accepted)]
        public IActionResult MostrarTemperaturaPorLocalizacao( string lat , string lon)
        {

            var resultado = new CidadeService(contextoCidade, contextoTemperatura).RetornaTemperaturaPorLocalizacao(lat,lon);
            if (resultado.Sucesso)
                return Ok(resultado);
            else
                return BadRequest(resultado);
        }

        [HttpGet]
        [Route("historico")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status202Accepted)]
        public IActionResult MostrarHistoricoTemperatura(string nomeCidade, string lat, string lon)
        {

            var resultado = new CidadeService(contextoCidade, contextoTemperatura).RetornarHistorico(nomeCidade,lat, lon);
            if (resultado.Sucesso)
                return Ok(resultado);
            else
                return BadRequest(resultado);
        }
    }
}
