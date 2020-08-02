using DesafioCsharpEasy.Dtos;
using DesafioCsharpEasy.Models;
using DesafioCsharpEasy.Repository;
using DesafioCsharpEasy.Services.OpenWeatherMap;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioCsharpEasy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private ICityTemperatureRepository _cityTempRepository;
        private IOpenWeatherMapService _openWeatherMapService;

        public TemperatureController(ICityTemperatureRepository cityTempRepository, IOpenWeatherMapService openWeatherMapService)
        {
            _cityTempRepository = cityTempRepository;
            _openWeatherMapService = openWeatherMapService;
        }

        [HttpGet("ByCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<double> GetTemperatureByCity([BindRequired] string city)
        {
            try
            {
                var result = _openWeatherMapService.GetTemperatureByCity(city.ToLower());

                if (result == null)
                    return NotFound();

                _cityTempRepository.Add(SetUpCityTemp(result));

                return Ok(result.Main.Temp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ByCoord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<double> GetTemperatureByCoord([BindRequired] double latitude, [BindRequired] double longitude)
        {
            try
            {
                var result = _openWeatherMapService.GetTemperatureByCoord(latitude.ToString(), longitude.ToString());

                if (result == null)
                    return NotFound();

                _cityTempRepository.Add(SetUpCityTemp(result));

                return Ok(result.Main.Temp);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("CurrentMonthHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HistoricalPresentation> GetCurrentMonthHistory(string city, string latitude, string longitude)
        {
            var firstDateCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDateCurrentMonth = firstDateCurrentMonth.AddMonths(1).AddDays(-1);

            return GetMonthHistory(city, latitude, longitude, firstDateCurrentMonth, lastDateCurrentMonth);
        }

        [HttpGet("LastMonthHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HistoricalPresentation> GetLastMonthHistory(string city, string latitude, string longitude)
        {
            var firstDateCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var fristDateLastMonth = firstDateCurrentMonth.AddMonths(-1);
            var lastDateLastMonth = firstDateCurrentMonth.AddDays(-1);

            return GetMonthHistory(city, latitude, longitude, fristDateLastMonth, lastDateLastMonth);
        }

        private ActionResult<HistoricalPresentation> GetMonthHistory(string city, string latitude, string longitude, DateTime startDate, DateTime endDate)
        {
            try
            {
                // Não implementei a chamada da API "OpenWeatherMaps" porque a ApiKey gratúita não tem acesso à históricos.

                if (string.IsNullOrEmpty(city) && (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude)))
                    return BadRequest();

                var cityTempList = new List<CityTemperature>();

                if (!string.IsNullOrEmpty(city))
                    cityTempList = _cityTempRepository.GetHistoricalLastMonth(city, startDate, endDate).ToList();
                else
                    cityTempList = _cityTempRepository.GetHistoricalLastMonth(latitude, longitude, startDate, endDate).ToList();

                if (cityTempList == null || cityTempList.Count() == 0)
                    return NotFound();

                var historicalDto = SetUpHistoricalDto(cityTempList);

                return Ok(historicalDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #region SetUps
        private CityTemperature SetUpCityTemp(CurrentModel result)
        {
            return new CityTemperature()
            {
                City = result.Name,
                Latitude = result.Coord.Lat.ToString(),
                Longitude = result.Coord.Lon.ToString(),
                DateTime = DateTime.Now,
                Temperature = result.Main.Temp
            };
        }

        private HistoricalPresentation SetUpHistoricalDto(List<CityTemperature> cityTempList)
        {
            var dto = new HistoricalPresentation()
            {
                City = cityTempList.FirstOrDefault().City
            };

            foreach (var t in cityTempList)
            {
                dto.DateTemperature.Add(
                    new DateTemperature()
                    {
                        Date = t.DateTime,
                        Temperature = t.Temperature,
                        Latutude = t.Latitude,
                        Longitude = t.Longitude
                    }
                );
            };

            return dto;
        }
        #endregion
    }
}
