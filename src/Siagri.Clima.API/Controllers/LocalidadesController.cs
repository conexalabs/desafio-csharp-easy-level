using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Siagri.Clima.Business.Dtos;
using Siagri.Clima.Business.Interfaces.Services;

namespace Siagri.Clima.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalidadesController : ControllerBase
    {
        private readonly ILocalidadeService _localidadeService;

        public LocalidadesController(ILocalidadeService localidadeService)
        {
            _localidadeService = localidadeService;
        }

        [HttpGet("ObterPorCoordenada")]
        public async Task<ActionResult<LocalidadeDto>> ObterPorCoordenada([FromQuery] CoordenadaDto coordenada)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var localidade = await _localidadeService.ObterPorCoordenada(coordenada);

                if (localidade != null)
                {
                    return Ok(localidade);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ObterPorCidade")]
        public async Task<ActionResult<LocalidadeDto>> ObterPorCidade([FromQuery] CidadeDto cidadeDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var result = await _localidadeService.ObterPorCidade(cidadeDto);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ObterPorCidadeOuCoordernada")]
        public async Task<ActionResult<HistoricoConsultasDto>> ObterPorCidadeOuCoordernada([FromQuery] string nome = "", string longitude = "", string latitude = "")
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var localidadeDto = new LocalidadeDto(nome, longitude, latitude);

                var result = await _localidadeService.ObterPorCidadeOuCoordenada(localidadeDto);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
