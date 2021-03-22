using AutoMapper;
using ClimaAPI.Application.Dtos;
using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Application.Mappers
{
    public class DtoToModelMappingRegistro : Profile
    {
        public DtoToModelMappingRegistro()
        {
            RegistroMap();
        }

        private void RegistroMap()
        {
            CreateMap<RegistroDto, Registro>()
                .ForMember(dest => dest.RegistroId, opt => opt.Ignore())
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.Temperatura, opt => opt.MapFrom(src => src.Temperatura))
                .ForMember(dest => dest.CidadeId, opt => opt.MapFrom(src => new Cidade
                {
                    Nome = src.CidadeNome
                }));
        }
    }
}
