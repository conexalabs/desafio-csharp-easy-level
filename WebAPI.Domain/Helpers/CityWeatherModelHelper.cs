using System;
using WebAPI.Domain.Models.Entities;
using WebAPI.Domain.Models.Service.OpenWeather;

namespace WebAPI.Domain.Helpers
{
    public static class CityWeatherModelHelper
    {
        public static CityWeather FromOpenWeatherToModel(CurrentModel model)
        {
            return new CityWeather {
                CityName = model.Name,
                Latitude = model.Coordinate.Latitude,
                Longitude = model.Coordinate.Longitude,
                Date = DateTimeOffset.FromUnixTimeSeconds(model.Date).DateTime.AddHours(-3),
                Temperature = model.Main.Temperature
                };
        }
    }
}
