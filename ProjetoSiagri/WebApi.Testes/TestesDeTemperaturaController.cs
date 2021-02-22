using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Testes
{
    public class TestesDeTemperaturaController
    {

        [Fact]
        public void TesteGetPorCidadeOk()
        {
            var controller = new TemperaturaController(null);
            var result = controller.GetPorCidade("Goiania");

            Assert.IsType(typeof(OkObjectResult), result);
        }

        [Fact]
        public void TesteGetPorCidadeBadRequest()
        {
            var controller = new TemperaturaController(null);
            var result = controller.GetPorCidade(null);

            Assert.IsType(typeof(BadRequestResult), result);
        }

        [Fact]
        public void TesteGetPorCidadeNotFound()
        {
            var controller = new TemperaturaController(null);
            var result = controller.GetPorCidade("Goianiaaa");

            Assert.IsType(typeof(StatusCodeResult), result);
            Assert.Equal(new NotFoundResult().StatusCode, ((StatusCodeResult)(result)).StatusCode);
        }

        [Fact]
        public void TesteGetPorGeolocalizacaoOk()
        {
            var controller = new TemperaturaController(null);
            var result = controller.GetPorGeolocalizacao(-16.3267, -48.9528);

            Assert.IsType(typeof(OkObjectResult), result);
        }
       

        [Fact]
        public void TesteGetPorGeolocalizacaoNotFound()
        {
            var controller = new TemperaturaController(null);
            var result = controller.GetPorGeolocalizacao(double.MinValue, double.MinValue);

            Assert.IsType(typeof(StatusCodeResult), result);
        }

        [Fact]
        public void TesteGetHistoricoTemperaturasPorGeolocalizacaoOk()
        {
            var controller = new TemperaturaController(null);
            var result = controller.GetHistoricoTemperaturasPorGeolocalizacao(-16.3267, -48.9528);

            Assert.IsType(typeof(OkObjectResult), result);
        }


        [Fact]
        public void TesteGetHistoricoTemperaturasPorGeolocalizacaoNotFound()
        {
            var controller = new TemperaturaController(null);
            var result = controller.GetPorGeolocalizacao(double.MinValue, double.MinValue);

            Assert.IsType(typeof(StatusCodeResult), result);
        }

        [Fact]
        public void TesteGetHistoricoTemperaturasPorCidadeOk()
        {
            var controller = new TemperaturaController(null);
            var result = controller.GetHistoricoTemperaturasPorCidade("anapolis");

            Assert.IsType(typeof(OkObjectResult), result);
        }


        [Fact]
        public void TesteGetHistoricoTemperaturasPorCidadeNotFound()
        {
            var controller = new TemperaturaController(null);
            var result = controller.GetHistoricoTemperaturasPorCidade("rio verde");

            Assert.IsType(typeof(NotFoundResult), result);
        }

    }
}
