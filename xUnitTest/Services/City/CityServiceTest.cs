using Application.Interfaces.Repository;
using Application.Services;
using Application.ViewModels.City.Response;
using AutoMapper;
using Infra.Data.APIExterna;
using Moq;
using Xunit;

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
            var cidade = "Sorriso";
            _apiExternalWeatherMapsMock.Setup(x => x.GetTempByCity(cidade, null))
                .ReturnsAsync(new CityViewModelResponse
                {
                    lat = "1",
                    lon = "2",
                    Nome = "Sorriso",
                    Temp = "10"
                });
        }
    }
}