using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Siagri.Clima.Business.Entities;

namespace Siagri.Clima.Business.Interfaces.Repositories
{
    public interface ILocalidadeRepository
    {
        Task<Localidade> Adicionar(Localidade localidade);

        Task<IEnumerable<Localidade>> ObterHistorico(string cidade, DateTime dataFiltro);
    }
}
