using AutoMapper;
using ClimaAPI.Application.Dtos;
using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Application.Mappers
{
    public class DtoToModelMappingCidade : Profile
    {
        public DtoToModelMappingCidade()
        {
            CidadeMap();
        }

        private void CidadeMap()
        {
            CreateMap<CidadeDto, Cidade>()
                .ForMember(dest => dest.CidadeId, opt => opt.Ignore())
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));
        }
    }
}
