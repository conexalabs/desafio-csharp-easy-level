using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Siagri.Clima.Business.Entities;
using Siagri.Clima.Business.VOs;

namespace Siagri.Clima.Business.Dtos
{
    public class LocalidadeDto
    {
        public LocalidadeDto() { }

        public LocalidadeDto(string nome = "", string longitude = "", string latitude = "")
        {
            Cidade = new CidadeDto(){Nome = nome};
            Coordenada = new CoordenadaDto() { Latitude = latitude, Longitude = longitude };
        }

        public Guid? LocalidadeId { get; set; }

        public CidadeDto Cidade { get; set; }

        public CoordenadaDto Coordenada { get; set; }

        public string TemperaturaAtual { get; set; }

        public DateTime? DataConsulta { get; set; }

        public static Localidade ToEntity(LocalidadeDto localidadeDto)
        {
            var localidade = new Localidade()
            {
                Cidade = new CidadeVO(){ Nome = localidadeDto.Cidade.Nome },
                Coordenada = new CoordenadaVO() { Longitude = localidadeDto.Coordenada.Longitude, Latitude = localidadeDto.Coordenada.Latitude},
                TemperaturaAtual = localidadeDto.TemperaturaAtual,
                DataConsulta = DateTime.Now,
            };

            return localidade;
        }

        public static LocalidadeDto ToDto(Localidade localidade)
        {
            var localidadeDto = new LocalidadeDto()
            {
                LocalidadeId = localidade.LocalidadeId,
                Cidade = new CidadeDto() { Nome = localidade.Cidade.Nome},
                Coordenada = new CoordenadaDto() { Latitude = localidade.Coordenada.Latitude, Longitude = localidade.Coordenada.Longitude},
                TemperaturaAtual = localidade.TemperaturaAtual,
                DataConsulta = DateTime.Now,
            };

            return localidadeDto;
        }

        public static async Task<LocalidadeDto> FromHttpResponseToDto(HttpResponseMessage message)
        {
            var parseObject = JObject.Parse(await message.Content.ReadAsStringAsync());

            var latitude = parseObject["coord"]["lat"].ToString();
            var longitude = parseObject["coord"]["lon"].ToString();
            var nome = parseObject["name"].ToString();
            var temperatura = parseObject["main"]["temp"].ToString();

            var localidadeDto = new LocalidadeDto()
            {
                Cidade = new CidadeDto() {Nome = nome},
                Coordenada = new CoordenadaDto() {Longitude = longitude, Latitude = latitude},
                TemperaturaAtual = temperatura,
                DataConsulta = DateTime.Now,
            };

            return localidadeDto;
        }
    }
}
