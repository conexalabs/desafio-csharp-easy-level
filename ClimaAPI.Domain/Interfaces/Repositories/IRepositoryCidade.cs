using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Domain.Interfaces.Repositories
{
    public interface IRepositoryCidade : IRepositoryBase<Cidade>
    {
        Cidade GetByName(string cidadeNome);
        Cidade GetByCoordinates(double latitude, double longitude);
    }
}
