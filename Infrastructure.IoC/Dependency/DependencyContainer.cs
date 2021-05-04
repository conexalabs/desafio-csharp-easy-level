using System.Reflection;
using Application.Interfaces.ExternaWeatherMaps;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.Services;
using Infra.Data.APIExterna;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.Dependency
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            serviceCollection.AddScoped<ICityService, CityService>();
            serviceCollection.AddScoped<ICityRepository, CityRepository>();
            serviceCollection.AddScoped<IApiExternalWeatherMaps, ApiExternalWeatherMaps>();
            serviceCollection.AddAutoMapper(Assembly.Load("Application"));
        }
    }
}