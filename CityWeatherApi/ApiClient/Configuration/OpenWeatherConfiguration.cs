using CityWeatherApi.ApiClient.Interfaces;
using CityWeatherApi.ApiClient.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.ApiClient.Configuration
{
    public static class OpenWeatherConfiguration
    {
        public static void AddApiOpenWeatherConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OpenWeatherOptions>(o => {
                o.BaseAddress = configuration["OpenWeatherCommunication:BaseAddress"];
                o.CityEndPoint = configuration["OpenWeatherCommunication:CityEndPoint"];
                o.LatLonEndPoint = configuration["OpenWeatherCommunication:LatLonEndPoint"];
                o.HistoricEndPoint = configuration["OpenWeatherCommunication:HistoricEndPoint"];
            });

            services.AddHttpClient<IOpenWeatherClient, OpenWeatherClient>();
        }
    }
}
