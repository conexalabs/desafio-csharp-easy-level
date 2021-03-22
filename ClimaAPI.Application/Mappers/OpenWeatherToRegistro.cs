using System;
using AutoMapper;
using ClimaAPI.Application.Dtos;
using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Application.Mappers
{
    public class OpenWeatherToRegistro : Profile
    {
        public OpenWeatherToRegistro()
        {
            RegistroMap();
        }

        private void RegistroMap()
        {
            CreateMap<OpenWeatherResposta, Registro>()
                .ForMember(dest => dest.RegistroId, opt => opt.Ignore())
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Temperatura, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.CidadeId, opt => opt.MapFrom(src => src.Id));
        }
    }
}

