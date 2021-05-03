using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Entidades.City;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.ViewModels.City;
using Application.ViewModels.City.Response;
using AutoMapper;
using Infra.Data.APIExterna;

namespace Application.Services
{
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;
        private IMapper _mapper;
        private IApiExternalWeatherMaps _apiExternalWeatherMaps;
        public CityService(ICityRepository cityRepository, IMapper mapper, IApiExternalWeatherMaps apiExternalWeatherMaps)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _apiExternalWeatherMaps = apiExternalWeatherMaps;
        }

        public async Task<CityViewModelResponse> GetTempCidade(string cidade)
        {
            CityViewModelResponse city = null;
            CityViewModel cityAPI = null;
            //Pegar da API , se não conseguir, tentar pegar pelo banco de dados;
            try
            {
                cityAPI = await _apiExternalWeatherMaps.GetTempByCity(cidade, "Metric");
            }
            catch (HttpRequestException ex)
            {
                var cidadeBanco = _cityRepository.GetByCidade(cidade);
                if (cidadeBanco == null)
                    throw new Exception(
                        "Não foi possível acessar a API externa e encontrar esses dados no banco local. Por favor, tente mais tarte");
                //mapping
                city.Mensagem = "API indisponível no momento, ultimo dado consultado foi de " +
                                cidadeBanco.UltimaAtualizacao + ".";
                return city;

            }
            //Adicionar no banco de dados o valor consultado.
            return city;

        }
        
        public IList<CityViewModel> GetAll()
        {
            return _mapper.Map<IList<CityViewModel>>(_cityRepository.GetAll());
        }


        public CityViewModel GetById(Guid id)
        {
            var city = _cityRepository.GetById(Guid.Parse("F212AA25-7E28-4F37-AFC8-50E618CEF3C9"));
            return _mapper.Map<CityViewModel>(city);
        }

        public CityViewModel Add(Entidades.City.City TEntity)
        {
            throw new NotImplementedException();
        }
    }
}