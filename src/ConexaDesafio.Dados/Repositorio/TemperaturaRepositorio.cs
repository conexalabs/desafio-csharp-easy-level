using ConexaDesafio.Dados.Contexto;
using ConexaDesafio.Negocio.Interfaces;
using ConexaDesafio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConexaDesafio.Dados.Repositorio
{
    public class TemperaturaRepositorio : RepositorioBase<Temperatura>
    {
        public TemperaturaRepositorio(ConexaDesafioDbContext db) : base(db)
        {
        }



    }
}
