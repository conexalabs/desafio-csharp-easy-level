using Autofac;
using AutoMapper;
using ClimaAPI.Application;
using ClimaAPI.Application.Interfaces;
using ClimaAPI.Application.Mappers;
using ClimaAPI.Domain.Interfaces.Repositories;
using ClimaAPI.Domain.Interfaces.Services;
using ClimaAPI.Domain.Services;
using ClimaAPI.Infrastructure.Data.Repositories;

namespace ClimaAPI.Infrastructure.CrossCutting.IOC
{
    public class ConfigurationIoc
    {
        public static void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationServiceRegistro>().As<IApplicationServiceRegistro>();
            builder.RegisterType<ServiceCidade>().As<IServiceCidade>();
            builder.RegisterType<ServiceRegistro>().As<IServiceRegistro>();
            builder.RegisterType<RepositoryCidade>().As<IRepositoryCidade>();
            builder.RegisterType<RepositoryRegistro>().As<IRepositoryRegistro>();
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelMappingCidade());
                cfg.AddProfile(new ModelToDtoMappingCidade());
                cfg.AddProfile(new DtoToModelMappingRegistro());
                cfg.AddProfile(new ModelToDtoMappingRegistro());
                cfg.AddProfile(new OpenWeatherToRegistroDto());
                cfg.AddProfile(new OpenWeatherToRegistro());
                cfg.AddProfile(new OpenWeatherToCidade());
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }

}