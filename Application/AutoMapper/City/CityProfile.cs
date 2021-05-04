using System;
using Application.Entidades.City;
using Application.ViewModels;
using Application.ViewModels.City;
using Application.ViewModels.City.Response;
using AutoMapper;

namespace Application.AutoMapper
{
    public class CityAutoMapper : Profile
    {
        public CityAutoMapper()
        {
            CreateMap<City, CityViewModelResponse>();
            CreateMap<City, City>();
            CreateMap<CityViewModelResponse, City>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Nome))
                .ForPath(x => x.coord.lat, y => y.MapFrom(z => z.lat))
                .ForPath(x => x.coord.lon, y => y.MapFrom(z => z.lon))
                .ForMember(x=>x.UltimaAtualizacao,y=>y.MapFrom(z=>DateTime.UtcNow));
                
            CreateMap<CityAPI, City>()
                .ForMember(x=>x.Name, y=>y.MapFrom(z=>z.name))
                .ForMember(x=>x.Temp, y=>y.MapFrom(z=>z.main.temp))
                .ForMember(x=>x.country, y=>y.MapFrom(z=>z.sys.country))
                .ForMember(x=>x.UltimaAtualizacao, y=>y.MapFrom(z=>DateTime.UtcNow))
                .ForMember(x=>x.coord, y=>y.MapFrom(z=>z.coord));
            CreateMap<CityAPI, CityViewModelResponse>().ForMember(x => x.Nome, y => y.MapFrom(z => z.name))
                .ForMember(x => x.Temp, y => y.MapFrom(z => z.main.temp))
                .ForMember(x => x.lat, y => y.MapFrom(z => z.coord.lat))
                .ForMember(x => x.lon, y => y.MapFrom(z => z.coord.lon));
        }
    }
}