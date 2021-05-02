using System.Reflection;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.Services;
using Infra.Data.DBContext;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.Dependency
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ConexaContext>(opt => opt.UseInMemoryDatabase("ConexaDB"));

            //Service
            serviceCollection.AddScoped<ICityService, CityService>();
            //Reposity
            serviceCollection.AddScoped<ICityRepository, CityRepository>();
            
            //Adiciona os AutoMapper
            serviceCollection.AddAutoMapper(Assembly.Load("Application"));
            

        }
    }
}