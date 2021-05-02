using System;
using System.Collections.Generic;
using Application.Entidades.City;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.ViewModels.City;
using AutoMapper;

namespace Application.Services
{
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;
        private IMapper _mapper;
        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public CityViewModel GetTempCidade()
        {
            return new CityViewModel()
            {
                Name = _cityRepository.GetById(Guid.NewGuid()).Name
            };
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