using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Service;
using Application.ViewModels.City;
using Application.ViewModels.City.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TempController : ControllerBase
    {

        private readonly ICityService _cityService;

        public TempController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("Cidade")]
        public async Task<IActionResult> Get(string cidade)
        {
            try
            {
                return Ok(await _cityService.GetTempCidade(cidade));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("lonlat")]
        public async Task<IActionResult> Getlonlat(string lat, string lon)
        {
            try
            {
                return Ok(await _cityService.GetTempCoord(lat,lon));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}