using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Entidades.City;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.ViewModels;
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
            CityViewModelResponse city;
            //Pegar da API , se não conseguir, tentar pegar pelo banco de dados;
            try
            {
                city = await _apiExternalWeatherMaps.GetTempByCity(cidade, "Metric");
            }catch (HttpRequestException ex)
            {
                var testebanco = _cityRepository.GetAll();
                var cidadeBanco = _cityRepository.GetByCidade(cidade);
                if (cidadeBanco == null)
                    throw new Exception(
                        "Não foi possível acessar a API externa e encontrar esses dados no banco local. Por favor, tente mais tarte");
                //mapping
                city = _mapper.Map<CityViewModelResponse>(cidadeBanco);
                city.Mensagem = "API indisponível no momento, ultimo dado consultado foi de " +
                                cidadeBanco.UltimaAtualizacao + ".";
                return city;

            }
            //Adicionar no banco de dados o valor consultado.
            try
            {
                var obj = _mapper.Map<City>(city);
                if(_cityRepository.GetByCidade(obj.Name)==null)
                    await Add(obj);
                else
                {
                    var objupdate = _cityRepository.GetByCidade(obj.Name);
                    objupdate.Temp = obj.Temp;
                    objupdate.coord = obj.coord;
                    objupdate.UltimaAtualizacao=DateTime.Now;
                    await Update(objupdate);
                }
                return city;
            }
            catch (Exception ex)
            {
                return city;
            }
            
        }

        public async Task<CityViewModelResponse> GetTempCoord(string lat, string lon)
        {
            CityViewModelResponse city;
            //Pegar da API , se não conseguir, tentar pegar pelo banco de dados;
            try
            {
                city = await _apiExternalWeatherMaps.GetTempByLonLat(lat, lon);
            }
            catch (HttpRequestException ex)
            {
                var cidadeBanco = _cityRepository.GetByLonLat(lon, lat);
                if (cidadeBanco == null)
                    throw new Exception(
                        "Não foi possível acessar a API externa e encontrar esses dados no banco local. Por favor, tente mais tarte");
                //mapping
                city = _mapper.Map<CityViewModelResponse>(cidadeBanco);
                city.Mensagem = "API indisponível no momento, ultimo dado consultado foi de " +
                                cidadeBanco.UltimaAtualizacao + ".";
                return city;

            }
            catch (Exception ex)
            {
                throw new Exception(
                    ex.Message);
            }
            //Adicionar no banco de dados o valor consultado.
            try
            {
                var allTeste = _cityRepository.GetAll();
                var obj = _mapper.Map<City>(city);
                if(!_cityRepository.AnyLonLat(obj.coord.lat, obj.coord.lon))
                    await Add(obj);
                else
                {
                    var objupdate = _cityRepository.GetByLonLat(obj.coord.lat, obj.coord.lon);
                    objupdate.Temp = obj.Temp;
                    objupdate.coord = obj.coord;
                    objupdate.UltimaAtualizacao=DateTime.Now;
                    objupdate.Name = obj.Name;
                    await Update(objupdate);
                }
                return city;
            }
            catch (Exception ex)
            {
                return city;
            }
        }


        public IList<CityViewModelResponse> GetAll()
        {
            return _mapper.Map<IList<CityViewModelResponse>>(_cityRepository.GetAll());
        }
        
        public async Task<City> Update(City t)
        {
            return await _cityRepository.Update(t);
        }

        private async Task Add(City TEntity)
        {
            _mapper.Map<CityViewModelResponse>(await _cityRepository.Add(TEntity));
        }
    }
}