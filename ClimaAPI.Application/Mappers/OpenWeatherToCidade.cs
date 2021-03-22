using AutoMapper;
using ClimaAPI.Application.Dtos;
using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Application.Mappers
{
    public class OpenWeatherToCidade : Profile
    {
        public OpenWeatherToCidade()
        {
            CidadeMap();
        }

        private void CidadeMap()
        {
            CreateMap<OpenWeatherResposta, Cidade>()
                .ForMember(dest => dest.CidadeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Coord.Lat))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Coord.Lon));
        }
    }
}
