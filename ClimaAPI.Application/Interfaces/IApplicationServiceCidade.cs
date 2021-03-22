using System.Collections.Generic;
using ClimaAPI.Application.Dtos;

namespace ClimaAPI.Application.Interfaces
{
    public interface IApplicationServiceCidade
    {
        void Add(CidadeDto cidadeDto);

        void Update(CidadeDto cidadeDto);

        void Remove(CidadeDto cidadeDto);

        IEnumerable<CidadeDto> GetAll();

        CidadeDto GetById(int id);
    }
}
