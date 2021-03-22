using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ClimaAPI.Application.Dtos;
using ClimaAPI.Application.Interfaces;
using ClimaAPI.Domain.Entities;
using ClimaAPI.Domain.Interfaces.Services;
using Newtonsoft.Json;

namespace ClimaAPI.Application
{
    public class ApplicationServiceRegistro : IApplicationServiceRegistro
    {
        private readonly IServiceRegistro _serviceRegistro;
        private readonly IServiceCidade _serviceCidade;
        private readonly IMapper _mapper;
        private const string ApiKey = "9360b219ac6d18f6488315c3a2ad339e";

        public ApplicationServiceRegistro(IServiceRegistro serviceRegistro
                                        , IServiceCidade serviceCidade, IMapper mapper)
        {
            _serviceRegistro = serviceRegistro;
            _serviceCidade = serviceCidade;
            _mapper = mapper;
        }

        public async Task<RegistroDto> Post(string cidadeNome)
        {
            var respostaApi = await ObterOpenWeatherResposta(cidadeNome);
            var registroDto = _mapper.Map<RegistroDto>(respostaApi);
            var registro = _mapper.Map<Registro>(respostaApi);
            var cidade = _mapper.Map<Cidade>(respostaApi);

            if (_serviceCidade.GetById(cidade.CidadeId) == null)
            {
                _serviceCidade.Add(cidade);
            }

            _serviceRegistro.Add(registro);

            return registroDto;
        }

        public async Task<RegistroDto> Post(double latitude, double longitude)
        {
            var respostaApi = await ObterOpenWeatherResposta(latitude, longitude);
            var registroDto = _mapper.Map<RegistroDto>(respostaApi);
            var registro = _mapper.Map<Registro>(respostaApi);
            var cidade = _mapper.Map<Cidade>(respostaApi);

            if (_serviceCidade.GetById(cidade.CidadeId) == null)
            {
                _serviceCidade.Add(cidade);
            }

            _serviceRegistro.Add(registro);

            return registroDto;
        }

        public IEnumerable<RegistroDto> GetHistorico(string cidadeNome)
        {
            var cidade = _serviceCidade.GetByName(cidadeNome);

            if (cidade == null) return new List<RegistroDto>();

            var registros = _serviceRegistro.GetHistorico(cidade.CidadeId);
            var registrosDto = ModelToDto(registros, cidade);

            return registrosDto;
        }

        public IEnumerable<RegistroDto> GetHistorico(double latitude, double longitude)
        {
            var cidade = _serviceCidade.GetByCoordinates(latitude, longitude);

            if (cidade == null) return new List<RegistroDto>();

            var registros = _serviceRegistro.GetHistorico(cidade.CidadeId);
            var registrosDto = ModelToDto(registros, cidade);

            return registrosDto;

        }

        private IEnumerable<RegistroDto> ModelToDto(IEnumerable<Registro> registros, Cidade cidade)
        {
            var registrosDto = _mapper.Map<IEnumerable<RegistroDto>>(registros).ToList();

            foreach (var registroDto in registrosDto)
            {
                registroDto.CidadeNome = cidade.Nome;
            }

            return registrosDto;
        }

        private static async Task<OpenWeatherResposta> ObterOpenWeatherResposta(string cidadeNome)
        {
            using var client = new HttpClient { BaseAddress = new Uri("http://api.openweathermap.org") };
            var resposta = await client.GetAsync($"/data/2.5/weather?q={cidadeNome}&units=metric&appid={ApiKey}");
            resposta.EnsureSuccessStatusCode();

            return await ConverterResposta(resposta);
        }

        private static async Task<OpenWeatherResposta> ObterOpenWeatherResposta(double latitude, double longitude)
        {
            using var client = new HttpClient { BaseAddress = new Uri("http://api.openweathermap.org") };
            var resposta = await client.GetAsync($"/data/2.5/weather?lat={latitude.ToString(new CultureInfo("en-US"))}" +
                                                 $"&lon={longitude.ToString(new CultureInfo("en-US"))}&units=metric&appid={ApiKey}");
            resposta.EnsureSuccessStatusCode();

            return await ConverterResposta(resposta);
        }

        private static async Task<OpenWeatherResposta> ConverterResposta(HttpResponseMessage resposta)
        {
            var stringResult = await resposta.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OpenWeatherResposta>(stringResult);
        }
    }
}
