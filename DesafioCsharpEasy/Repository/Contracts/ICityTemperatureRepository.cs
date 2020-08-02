using DesafioCsharpEasy.Models;
using System;
using System.Collections.Generic;

namespace DesafioCsharpEasy.Repository
{
    public interface ICityTemperatureRepository : IRepository<CityTemperature>
    {
        CityTemperature GetByCity(string city);
        IEnumerable<CityTemperature> GetHistoricalLastMonth(string city, DateTime startDate, DateTime endDate);
        IEnumerable<CityTemperature> GetHistoricalLastMonth(string latutude, string longitude, DateTime startDate, DateTime endDate);
    }
}
