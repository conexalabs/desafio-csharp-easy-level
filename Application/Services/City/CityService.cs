using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Entidades.City;
using Application.Enum;
using Application.Interfaces.ExternaWeatherMaps;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.ViewModels.City.Response;
using AutoMapper;

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
            var cityResponse = new CityViewModelResponse();
            try
            {
                cityResponse = await _apiExternalWeatherMaps.GetTempByCity(cidade);
            }catch (HttpRequestException)
            {
                var cityEntity = _cityRepository.GetByCidade(cidade);
                if (cityEntity == null)
                    throw new Exception(MessagerAPI.APIandBDNotDate.ToString());
                cityResponse = _mapper.Map<CityViewModelResponse>(cityEntity);
                cityResponse.Mensagem = "API indisponível no momento, ultimo dado consultado foi de " +
                                        cityEntity.UltimaAtualizacao + ".";
                return cityResponse;
            }
            try
            {
                var obj = _mapper.Map<City>(cityResponse);
                if(_cityRepository.GetByCidade(obj.CityName)==null)
                    await Add(obj);
                else
                {
                    var objupdate = _cityRepository.GetByCidade(obj.CityName);
                    objupdate.Temp = obj.Temp;
                    objupdate.coord = obj.coord;
                    objupdate.UltimaAtualizacao=DateTime.Now;
                    await Update(objupdate);
                }
                return cityResponse;
            }
            catch (Exception)
            {
                return cityResponse;
            }
            
        }

        public async Task<CityViewModelResponse> GetTempCoord(string lat, string lon)
        {
            CityViewModelResponse city;
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
            try
            {
                var obj = _mapper.Map<City>(city);
                if(!_cityRepository.AnyLonLat(obj.coord.lat, obj.coord.lon))
                    await Add(obj);
                else
                {
                    var objupdate = _cityRepository.GetByLonLat(obj.coord.lat, obj.coord.lon);
                    objupdate.Temp = obj.Temp;
                    objupdate.coord = obj.coord;
                    objupdate.UltimaAtualizacao=DateTime.Now;
                    objupdate.CityName = obj.CityName;
                    await Update(objupdate);
                }
                return city;
            }
            catch (Exception)
            {
                return city;
            }
        }


        public IList<CityViewModelResponse> GetAll()
        {
            return _mapper.Map<IList<CityViewModelResponse>>(_cityRepository.GetAll());
        }

        private async Task Update(City t)
        {
            await _cityRepository.Update(t);
        }

        private async Task Add(City entity)
        {
            _mapper.Map<CityViewModelResponse>(await _cityRepository.Add(entity));
        }
    }
}