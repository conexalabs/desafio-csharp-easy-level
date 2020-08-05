using ConexaDesafio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConexaDesafio.Negocio.Interfaces
{
    public interface IRepositorioBase<TEntity> : IDisposable
    {
        Task Adicionar(TEntity entidade);
    }
}
