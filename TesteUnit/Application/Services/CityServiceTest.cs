using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Infra.Data.APIExterna;
using Moq;
using Xunit;

namespace TesteUnit.Application.Services
{
    public class CityServiceTest
    {
        private ICityService _cityService;
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<IApiExternalWeatherMaps> _apiExternalWeatherMapsMock; 
        public CityServiceTest(ICityService cityService, Mock<ICityRepository> cityRepositoryMock, Mock<IApiExternalWeatherMaps> apiExternalWeatherMapsMock)
        {
            _cityService = cityService;
            _cityRepositoryMock = cityRepositoryMock;
            _apiExternalWeatherMapsMock = apiExternalWeatherMapsMock;
        }
        
        [Fact]
        public async void Add_Buscar_Cidade_Correta()
        {
            
            //arrange 
            var contaTest = new Conta
            {
                Nome="Nome Teste",
                DataPagamento = new DateTime(1998,05,10),
                DataVencimento =new DateTime(2000,05,10),
                ValorOriginal = 1000
            };

            _contaRepositoryMock.Setup(x => x.Add(contaTest))
                .ReturnsAsync(contaTest);
            
            // action

            var contaResult = await _contaService.Add(contaTest);

            // assert 
            _contaRepositoryMock.Verify(x=>x.Add(contaTest), Times.Once);
            Assert.True(contaResult.ValorCorrigido>0.0);
            Assert.False(contaResult.Status==StatusEnum.PagoComAtraso);
        }
    }
}