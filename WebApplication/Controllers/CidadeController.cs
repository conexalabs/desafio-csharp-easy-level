using System;
using System.Collections.Generic;
using Application.Interfaces.Service;
using Application.ViewModels.City;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CidadeController : ControllerBase
    {

        private readonly ICityService _cityService;

        public CidadeController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Route("{id}")]
        public CityViewModel Get([FromRoute] Guid id)
        {
            return _cityService.GetById(id);
        }
        [HttpGet]
        public IList<CityViewModel> GetAll()
        {
            return _cityService.GetAll();
        }
    }
}