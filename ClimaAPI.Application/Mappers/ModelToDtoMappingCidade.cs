using AutoMapper;
using ClimaAPI.Application.Dtos;
using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Application.Mappers
{
    public class ModelToDtoMappingCidade : Profile
    {

        public ModelToDtoMappingCidade()
        {
            CidadeDtoMap();
        }

        private void CidadeDtoMap()
        {
            CreateMap<Cidade, CidadeDto>()
                .ForMember(dest => dest.CidadeId, opt => opt.Ignore())
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));
        }
    }
}
