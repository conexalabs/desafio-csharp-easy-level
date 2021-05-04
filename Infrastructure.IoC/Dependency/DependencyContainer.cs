using System.Reflection;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.Services;
using Infra.Data.APIExterna;
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

            serviceCollection.AddHttpClient();
            serviceCollection.AddDbContext<ConexaContext>(opt => opt.UseSqlServer(
                @"Server=localhost,1433;Database=ConexaDB;User Id=SA;Password=0rUOw5M5ad; Integrated Security=True"));
    
            //Service
            serviceCollection.AddScoped<ICityService, CityService>();
            //Reposity
            serviceCollection.AddScoped<ICityRepository, CityRepository>();
            
            //Adiciona API Externa
            serviceCollection.AddScoped<IApiExternalWeatherMaps, ApiExternalWeatherMaps>();
            //Adiciona os AutoMapper
            serviceCollection.AddAutoMapper(Assembly.Load("Application"));
            
        }
    }
}