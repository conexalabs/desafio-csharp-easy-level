using System;
using System.Net.Http;
using Application.Entidades.City;
using Application.Interfaces.Repository;
using Application.Services;
using Application.ViewModels.City.Response;
using AutoMapper;
using Infra.Data.APIExterna;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace xUnitTest.Services.City
{
    public class CityServiceTest
    {
        private readonly CityService _cityService;
        private readonly Mock<ICityRepository> _cityRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IApiExternalWeatherMaps> _apiExternalWeatherMapsMock;
        public CityServiceTest()
        {
            
            _cityRepositoryMock= new Mock<ICityRepository>();
            _mapperMock= new Mock<IMapper>();
            _apiExternalWeatherMapsMock= new Mock<IApiExternalWeatherMaps>();
            
            _cityService = new CityService(_cityRepositoryMock.Object, _mapperMock.Object, _apiExternalWeatherMapsMock.Object);
        }


        [Fact]
        public void GetCidadeValida_Sucesso()
        {
            var objCityViewModel = new CityViewModelResponse()
            {
                Nome= "Sorriso",
                Temp= "26.67",
                lon= "-46.5373",
                lat= "-18.5653",
                Mensagem= "Valor buscado pelo API"
            };
            var cidade = "Sorriso";
            _apiExternalWeatherMapsMock.Setup(x => x.GetTempByCity(cidade, null))
                .ReturnsAsync(objCityViewModel);
            _mapperMock.Setup(x => x.Map<Application.Entidades.City.City>(objCityViewModel))
                .Returns(new Application.Entidades.City.City
            {
                Name = "Sorriso",
                Temp = "26.67",
                coord = new Coord()
                {
                    lat = "-18.5653",
                    lon = "-46.5373"
                }
            });
            _cityRepositoryMock.Setup(x => x.Add(new Application.Entidades.City.City()
            {
                Name = "Sorriso",
                Temp = "26.67",
                coord = new Coord()
                {
                    lat = "-18.5653",
                    lon = "-46.5373"
                }
            }));
            _cityRepositoryMock.Setup(x => x.GetByCidade("Sorriso")).Returns(new Application.Entidades.City.City()
            {
                Name = "Sorriso",
                Temp = "26.67",
                coord = new Coord()
                {
                    lat = "-18.5653",
                    lon = "-46.5373"
                }
            });
            
            _cityRepositoryMock.Verify(x => x.GetByCidade(cidade), Times.AtMostOnce);
            _apiExternalWeatherMapsMock.Verify(x => x.GetTempByCity(cidade, null), Times.AtMostOnce);
            var result = _cityService.GetTempCidade(cidade);

            Assert.True(result.Result.Nome=="Sorriso");
            Assert.True(result.Result.Temp=="26.67");
        }
        [Fact]
        public void GetCidadeValida_APIInvalida()
        {
            var cidade = "Sorriso";
            _apiExternalWeatherMapsMock.Setup(x => x.GetTempByCity(cidade, null))
                .Throws(new HttpRequestException("Mensagem"));
            _cityRepositoryMock.Setup(x => x.GetByCidade("Sorriso")).Returns(() => null);
            var result = _cityService.GetTempCidade(cidade);
            
            _cityRepositoryMock.Verify(x=>x.GetByCidade(cidade),Times.Once);
            Assert.Equal(result.Exception.InnerExceptions[0].Message, "Não foi possível acessar a API externa e encontrar esses dados no banco local. Por favor, tente mais tarte");
        }

        [Fact]
        public void GetCidadeValida_APIInvalidaComDados()
        {
            var cidade = "Sorriso";
            _apiExternalWeatherMapsMock.Setup(x => x.GetTempByCity(cidade, null))
                .Throws(new HttpRequestException("Mensagem"));
            var cityEntity = new Application.Entidades.City.City()
            {
                Name = "Sorriso",
                Temp = "26.67",
                coord = new Coord()
                {
                    lat = "-18.5653",
                    lon = "-46.5373"
                },
                UltimaAtualizacao = DateTime.Now
            };
            var objMappe = new CityViewModelResponse()
            {
                Nome = "Sorriso",
                Temp = "26.67",
                lat = "-18.5653",
                lon = "-46.5373",
                Mensagem = "API indisponível no momento, ultimo dado consultado foi de " +
                           cityEntity.UltimaAtualizacao + "."
            };
            _mapperMock.Setup(x => x.Map<CityViewModelResponse>(cityEntity))
                .Returns(objMappe);
            _cityRepositoryMock.Setup(x => x.GetByCidade("Sorriso")).Returns(cityEntity);
            var result = _cityService.GetTempCidade(cidade);
            _cityRepositoryMock.Verify(x=>x.GetByCidade(cidade),Times.Once);
            Assert.Equal(result.Result, objMappe);
        }
    }
}