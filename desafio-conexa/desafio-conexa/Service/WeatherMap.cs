using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace desafio_conexa.Service
{
    public class WeatherMap
    {
        public Weather getObjetoApiCidade(string nomeCidade)
        {
            return getApiWeather("q="+nomeCidade);
        }

        public Weather getApiWeather(string param)
        {
            using (var cliente = new HttpClient())
            {
                var resposta = cliente.GetAsync("http://api.openweathermap.org/data/2.5/weather?" + param + "&appid=2bc4a4e88c6885b48767c0be6edfd467&units=metric").Result;

                if (resposta.IsSuccessStatusCode)
                {
                    var obj = resposta.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Weather>(obj);

                }
                return null;
            }
        }

        public Weather getApiLatLong(string lat, string lon)
        {
            return getApiWeather($"lat={lat}&lon={lon}");
        }
    }
}
