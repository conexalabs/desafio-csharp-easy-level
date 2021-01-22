using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebAPI.Data.Interfaces;
using WebAPI.Data.Interfaces.Service.Models.Entities;
using WebAPI.Domain.Helpers;
using WebAPI.Domain.Interfaces.Response.CityWeather;
using WebAPI.Domain.Interfaces.Service.OpenWeather;
using WebAPI.Domain.Interfaces.Service.Validation;
using WebAPI.Domain.Models.Entities;
using WebAPI.Domain.Models.Response;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IUnitOfWork _unity;
        private readonly IOpenWeatherService _openWeatherService;
        private readonly ICityHystoricalWeatherService _cityHystoricalWeatherService;
        private readonly ICityWeatherActionResult _cityWeatherResponse;
        private readonly IValidations _validation;

        public WeatherController(IUnitOfWork unity, IOpenWeatherService openWeatherService,
            ICityWeatherActionResult cityWeatherResponse, ICityHystoricalWeatherService cityHystoricalWeatherService,
            IValidations validation)
        {
            _unity = unity;
            _openWeatherService = openWeatherService;
            _cityWeatherResponse = cityWeatherResponse;
            _cityHystoricalWeatherService = cityHystoricalWeatherService;
            _validation = validation;
        }

        // GET api/Weather/ByCityName
        [HttpGet("ByCityName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityWeatherResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationResult>))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(CityWeatherResponseModel))]
        public IActionResult GetWeatherByCityName([FromQuery] string cityname)
        {

            var errors = _validation.ValidateCityName(cityname);

            if (errors.Count() != 0)
                return StatusCode(StatusCodes.Status400BadRequest, errors);

            var response = _openWeatherService.GetTemperatureByCityName(cityname);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,                
                    _cityWeatherResponse.TemperatureResponse<CityWeatherResponseModel>(                
                        new CityWeatherResponseModel()
                        ));
            }
            else
            {

                var model = CityWeatherModelHelper.FromOpenWeatherToModel(response);
                errors = _validation.ValidateModel<CityWeather>(model);
                if (errors.Count() != 0)
                    return StatusCode(StatusCodes.Status400BadRequest, errors);

                _unity.CityWeatherRepository.Add(model);
                _unity.Commit();
                
                return Ok(
                _cityWeatherResponse.TemperatureResponse<CityWeatherResponseModel>(
                    new CityWeatherResponseModel().ModelToResponse<CityWeather>(model)
                    )
                );
            }
        }

        // GET api/Weather/ByGeoCoordinates
        [HttpGet("ByGeoCoordinates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityWeatherResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationResult>))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(CityWeatherResponseModel))]
        public IActionResult GetWeatherByGeoCoordinates([FromQuery] string latitude, [FromQuery] string longitude)
        {
            var errors = _validation.ValidateLatitudeLongitude(_validation.IsNumber(latitude), _validation.IsNumber(longitude));

            if (errors.Count() != 0)
                return StatusCode(StatusCodes.Status400BadRequest, errors);

            var response = _openWeatherService.GetTemperatureByGeoCoordinates(latitude, longitude);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
            else
            {
                var model = CityWeatherModelHelper.FromOpenWeatherToModel(response);
                errors = _validation.ValidateModel<CityWeather>(model);
                if (errors.Count() != 0)
                    return StatusCode(StatusCodes.Status400BadRequest, errors);

                _unity.CityWeatherRepository.Add(model);
                _unity.Commit();


                return Ok(
                    _cityWeatherResponse.TemperatureResponse<CityWeatherResponseModel>(
                        new CityWeatherResponseModel().ModelToResponse<CityWeather>(model)
                        )
                    );
            }
        }

        // GET api/Weather/MonthWeatherByCityName
        [HttpGet("MonthWeatherByCityName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CityWeather>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationResult>))]
        public IActionResult GetMonthWeatherByCityName([FromQuery] string cityname)
        {
            var errors = _validation.ValidateCityName(cityname);
            
            if(errors.Count() != 0)
                return StatusCode(StatusCodes.Status400BadRequest, errors);

            var res = _cityHystoricalWeatherService.GetWeathersFromMonth(cityname);
            return Ok(res);
        }

        // GET api/Weather/MonthWeatherByGeoCoordinates
        [HttpGet("MonthWeatherByGeoCoordinates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CityWeather>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationResult>))]
        public IActionResult GetMonthWeatherByGeoCoordinates([FromQuery] string latitude, [FromQuery] string longitude)
        {
            
            var errors = _validation.ValidateLatitudeLongitude(_validation.IsNumber(latitude), _validation.IsNumber(longitude));
            
            if (errors.Count() != 0)
                return StatusCode(StatusCodes.Status400BadRequest, errors);

            latitude = latitude.Replace('.', ',');
            longitude = longitude.Replace('.', ',');

            double latitudeNumber;
            double longitudeNumber;

            double.TryParse(latitude, out latitudeNumber); 
            double.TryParse(longitude, out longitudeNumber);

            var res = _cityHystoricalWeatherService.GetWeathersFromMonth(latitudeNumber, longitudeNumber);
            
            return Ok(res);
        }
    }
}
