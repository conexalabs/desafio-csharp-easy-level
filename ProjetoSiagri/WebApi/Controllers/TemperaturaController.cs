using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using WebApi.Repositorios;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperaturaController : ControllerBase
    {
        private readonly ILogger<TemperaturaController> _logger;
        private readonly HttpClient client = new HttpClient();

        public TemperaturaController(ILogger<TemperaturaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("cidade")]
        public ActionResult GetPorCidade(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest();
            }

            var requestUri = $"https://api.openweathermap.org/data/2.5/weather?q=" + nome + "&appid=8890bad197fa251c8d7fbccf24dddf3c&units=metric";

            return ProcessaResposta(requestUri);
        }

        [HttpGet]
        [Route("geolocalizacao")]
        public ActionResult GetPorGeolocalizacao(double lat, double lon)
        {
            var requestUri = $"https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=8890bad197fa251c8d7fbccf24dddf3c&units=metric";

            return ProcessaResposta(requestUri);
        }

        [HttpGet]
        [Route("historicoTemperaturasPorGeolocalizacao")]
        public ActionResult GetHistoricoTemperaturasPorGeolocalizacao(double lat, double lon)
        {
            var requestUri = $"https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=8890bad197fa251c8d7fbccf24dddf3c&units=metric";

            return ProcessaRespostaHistorico(requestUri);
        }

        [HttpGet]
        [Route("historicoTemperaturasPorCidade")]
        public ActionResult GetHistoricoTemperaturasPorCidade(string nome)
        {
            var requestUri = $"https://api.openweathermap.org/data/2.5/weather?q=" + nome + "&appid=8890bad197fa251c8d7fbccf24dddf3c&units=metric";

            return ProcessaRespostaHistorico(requestUri);
        }

        #region "métodos privados"
            private void ObtenhaIntervaloDePesquisa(out DateTime dtInicial, out DateTime dtFinal)
            {
                dtInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                dtFinal = dtInicial.AddMonths(1).AddDays(-1);
            }

            private ActionResult ProcessaResposta(string requestUri)
            {
                var httpResponse = client.GetAsync(requestUri).Result;

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var conteudoResposta = httpResponse.Content.ReadAsStringAsync().Result;

                    var respostaOpenWeather = JsonSerializer.Deserialize<OpenWeatherResultado>(conteudoResposta);

                    var repositorioTemperatura = new RepositorioDeTemperatura();

                    repositorioTemperatura.Salvar(respostaOpenWeather);

                    var resposta = new DadosTemperatura { DataHoraLeitura = DateTime.UtcNow, NomeCidade = respostaOpenWeather.name, TemperatureC = respostaOpenWeather.main.temp };

                    return new OkObjectResult(resposta);
                }

                return new StatusCodeResult((int)httpResponse.StatusCode);
            }

            private ActionResult ProcessaRespostaHistorico(string requestUri)
            {
                DateTime dataInicialMesAnterior, dataFinalMesAnterior;

                ObtenhaIntervaloDePesquisa(out dataInicialMesAnterior, out dataFinalMesAnterior);

                var httpResponse = client.GetAsync(requestUri).Result;

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var conteudoResposta = httpResponse.Content.ReadAsStringAsync().Result;

                    var respostaOpenWeather = JsonSerializer.Deserialize<OpenWeatherResultado>(conteudoResposta);

                    var repTemp = new RepositorioDeTemperatura();

                    var historico = new List<OpenWeatherResultado>();

                    historico = repTemp.Consultar(respostaOpenWeather.id, dataInicialMesAnterior, dataFinalMesAnterior);

                    if (historico.Count > 0)
                    {
                        return new OkObjectResult(historico);
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                return new StatusCodeResult((int)httpResponse.StatusCode);
            }
            #endregion

        }
}
