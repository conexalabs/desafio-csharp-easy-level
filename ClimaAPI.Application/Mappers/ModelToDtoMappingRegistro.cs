using AutoMapper;
using ClimaAPI.Application.Dtos;
using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Application.Mappers
{
    public class ModelToDtoMappingRegistro : Profile
    {
        public ModelToDtoMappingRegistro()
        {
            RegistroDtoMap();
        }
        private void RegistroDtoMap()
        {
            CreateMap<Registro, RegistroDto>()
                .ForMember(dest => dest.Temperatura, opt => opt.MapFrom(src => src.Temperatura))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.CidadeNome, opt => opt.MapFrom(src => new Cidade
                {
                    CidadeId = src.CidadeId
                }));
        }
    }
}
