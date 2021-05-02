using Application.Entidades.City;
using Application.ViewModels.City;
using AutoMapper;

namespace Application.AutoMapper
{
    public class CityAutoMapper : Profile
    {
        public CityAutoMapper()
        {
            CreateMap<City, CityViewModel>();
            CreateMap<CityViewModel, City>();
        }
    }
}