using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Domain.Interfaces.Services
{
    public interface IServiceCidade : IServiceBase<Cidade>
    {
        Cidade GetByName(string cidadeNome);
        Cidade GetByCoordinates(double latitude, double longitude);
    }
}
