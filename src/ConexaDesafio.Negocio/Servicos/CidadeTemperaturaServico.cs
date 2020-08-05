using ConexaDesafio.Negocio.Interfaces;
using ConexaDesafio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConexaDesafio.Negocio.Servicos
{
    public class CidadeTemperaturaServico
    {
        private readonly string _key;
        private readonly HttpClient _client;

        public CidadeTemperaturaServico()
        {
            _key = "e409aa5e8f54e87028619bec768bceff";
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
        }

        public async Task<string> MonteResultadoApi(string cidade)
        {
            if (_client.BaseAddress == null)
            {
                _client.BaseAddress = new Uri("http://api.openweathermap.org");
            }

            var respostaCidad = await ObtenhaRespostaApiCidade(cidade);
            respostaCidad.EnsureSuccessStatusCode();
            var conteudoResposta = await respostaCidad.Content.ReadAsStringAsync();

            return conteudoResposta;
        }

        public async Task<string> MonteResultadoApiLatLon(string lat, string lon)
        {
            if (_client.BaseAddress == null)
            {
                _client.BaseAddress = new Uri("http://api.openweathermap.org");
            }

            var respostaCidad = await ObtenhaRespostaApiLatLon(lat, lon);
            respostaCidad.EnsureSuccessStatusCode();
            var conteudoResposta = await respostaCidad.Content.ReadAsStringAsync();

            return conteudoResposta;
        }


        private async Task<HttpResponseMessage> ObtenhaRespostaApiCidade(string cidade)
        {
            return await _client.GetAsync($"/data/2.5/weather?q={cidade}&appid={_key}&units=metric");
        }

        private async Task<HttpResponseMessage> ObtenhaRespostaApiLatLon(string lat, string lon)
        {
            var t = $"api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&lang=pt_br&units=metric&appid={_key}";

            return await _client.GetAsync(t);
        }
    }
}
