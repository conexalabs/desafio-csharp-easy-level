using ClimaAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClimaAPI.Controllers
{
    [Route("/api/clima")]
    [ApiController]
    public class RegistrosController : ControllerBase
    {
        private readonly IApplicationServiceRegistro _applicationServiceRegistro;

        public RegistrosController(IApplicationServiceRegistro applicationServiceRegistro)
        {
            _applicationServiceRegistro = applicationServiceRegistro;
        }

        /// <summary>
        /// Recebe o nome da cidade, faz a persistência do resultado e retorna a temperatura atual.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST
        ///     https://localhost:5000/api/clima/Goiânia
        /// 
        /// </remarks>
        /// <param name="nomeDaCidade"></param>
        /// <returns>Se não houver erros, retorna o registro com a data e temperatura atuais e o nome da cidade.</returns>
        [HttpPost("{nomeDaCidade}")]
        public ActionResult<string> Post(string nomeDaCidade)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(_applicationServiceRegistro.Post(nomeDaCidade).Result);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Recebe a latitude e longitude da cidade, faz a persistência do resultado e retorna a temperatura atual.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST
        ///     https://localhost:5000/api/clima?latitude=12&amp;longitude=12
        /// 
        /// </remarks>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>Se não houver erros, retorna o registro com a data e temperatura atuais e o nome da cidade.</returns>
        [HttpPost]
        public ActionResult<string> Post([FromQuery] double latitude, [FromQuery] double longitude)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(_applicationServiceRegistro.Post(latitude, longitude).Result);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Recebe o nome da cidade ou a latitude e a longitude, faz a persistência do resultado e retorna a temperatura atual.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET
        ///     https://localhost:5000/api/clima/GetHistorico?nomeDaCidade=Goiânia
        ///     https://localhost:5000/api/clima/GetHistorico?latitude=12&amp;longitude=12
        /// 
        /// </remarks>
        /// <param name="nomeDaCidade"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>Se não houver erros, retorna o histórico de registros do último mês com as datas, as temperaturas e o nome da cidade.</returns>
        [HttpGet("[action]")]
        public ActionResult<string> GetHistorico([FromQuery] string nomeDaCidade, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(string.IsNullOrEmpty(nomeDaCidade)
                    ? _applicationServiceRegistro.GetHistorico(latitude, longitude)
                    : _applicationServiceRegistro.GetHistorico(nomeDaCidade));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
