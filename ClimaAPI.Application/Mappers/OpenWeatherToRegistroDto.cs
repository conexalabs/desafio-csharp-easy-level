using System;
using AutoMapper;
using ClimaAPI.Application.Dtos;

namespace ClimaAPI.Application.Mappers
{
    public class OpenWeatherToRegistroDto : Profile
    {
        public OpenWeatherToRegistroDto()
        {
            RegistroDtoMap();
        }

        private void RegistroDtoMap()
        {
            CreateMap<OpenWeatherResposta, RegistroDto>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Temperatura, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.CidadeNome, opt => opt.MapFrom(src => src.Name));
        }
    }
}

