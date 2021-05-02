using System;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.ViewModels.City;

namespace Application.Services.City
{
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public CityViewModel GetTempCidade()
        {
            return new CityViewModel()
            {
                Name = _cityRepository.GetById(Guid.NewGuid()).Name
            };
        }
    }
}