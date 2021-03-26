using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Siagri.Clima.Business.Constants;
using Siagri.Clima.Business.Dtos;
using Siagri.Clima.Business.Entities;
using Siagri.Clima.Business.Interfaces.Repositories;
using Siagri.Clima.Business.Interfaces.Services;

namespace Siagri.Clima.Business.Services
{
    public class LocalidadeService : ILocalidadeService
    {
        private static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(SiagriSettings.OpenWeatherUrl)
        };

        private readonly ILocalidadeRepository _localidadeRepository;

        public LocalidadeService(ILocalidadeRepository localidadeRepository)
        {
            _localidadeRepository = localidadeRepository;
        }

        #region Obter
        public async Task<LocalidadeDto> ObterPorCoordenada(CoordenadaDto coordenadaDto)
        {
            var localidadeDto = await ObterLocalidade(coordenadaDto);

            if (localidadeDto == null) return default;

            var result = await Adicionar(localidadeDto);

            return result;
        }

        public async Task<LocalidadeDto> ObterPorCidade(CidadeDto cidadeDto)
        {
            var localidadeDto = await ObterLocalidade(cidadeDto);

            if (localidadeDto == null) return default;
            
            var result = await Adicionar(localidadeDto);

            return result;
        }

        public async Task<HistoricoConsultasDto> ObterPorCidadeOuCoordenada(LocalidadeDto localidadeDto)
        {
            var response = !string.IsNullOrEmpty(localidadeDto.Cidade.Nome) 
                ? await ObterLocalidade(new CidadeDto() { Nome = localidadeDto.Cidade.Nome }) 
                : await ObterLocalidade(new CoordenadaDto() { Latitude = localidadeDto.Coordenada.Latitude, Longitude = localidadeDto.Coordenada.Longitude });
                

            if (response == null) return default;

            var dataFiltro = DateTime.Now.AddDays(-30);

            var localidades = await _localidadeRepository.ObterHistorico(response.Cidade.Nome, dataFiltro);

            var localidadesDto = localidades.Select(LocalidadeDto.ToDto);

            var historico = new HistoricoConsultasDto(localidadesDto);

            return historico;
        }

        #endregion

        #region Adicionar
        public async Task<LocalidadeDto> Adicionar(LocalidadeDto localidadeDto)
        {
            if (localidadeDto == null) return default;

            var localidade = LocalidadeDto.ToEntity(localidadeDto);

            return LocalidadeDto.ToDto(await _localidadeRepository.Adicionar(localidade));
        }

        #endregion

        #region Privados

        private async Task<LocalidadeDto> ObterLocalidade(CidadeDto cidadeDto)
        {
            var responseHttp = await _httpClient.GetAsync($"weather?q={cidadeDto.Nome}&units=metric&appid={SiagriSettings.ApiKey}");

            if (!responseHttp.IsSuccessStatusCode) return default;

            var localidadeDto = await LocalidadeDto.FromHttpResponseToDto(responseHttp);

            return localidadeDto;
        }

        private async Task<LocalidadeDto> ObterLocalidade(CoordenadaDto coordenadaDto)
        {
            var responseHttp = await _httpClient.GetAsync($"weather?lat={coordenadaDto.Latitude}&lon={coordenadaDto.Longitude}&units=metric&appid={SiagriSettings.ApiKey}");

            if (!responseHttp.IsSuccessStatusCode) return default;

            var localidadeDto = await LocalidadeDto.FromHttpResponseToDto(responseHttp);

            return localidadeDto;
        }

        #endregion

    }
}