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
    public class CidadeController : ControllerBase
    {

        private readonly ICityService _cityService;

        public CidadeController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
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
    }
}