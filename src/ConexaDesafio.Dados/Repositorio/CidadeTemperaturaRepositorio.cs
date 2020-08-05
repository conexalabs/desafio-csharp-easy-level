using ConexaDesafio.Dados.Contexto;
using ConexaDesafio.Negocio.Interfaces;
using ConexaDesafio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConexaDesafio.Dados.Repositorio
{
    public class CidadeTemperaturaRepositorio : RepositorioBase<CidadeTemperatura>, ICidadeTemperaturaRepositorio
    {
        public CidadeTemperaturaRepositorio(ConexaDesafioDbContext db) : base(db)
        {
        }
    }
}
