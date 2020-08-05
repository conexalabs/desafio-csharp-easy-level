using ConexaDesafio.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConexaDesafio.Dados.Contexto
{
    public class ConexaDesafioDbContext : DbContext
    {
        public ConexaDesafioDbContext(DbContextOptions<ConexaDesafioDbContext> options) : base(options) { }

        public DbSet<CidadeTemperatura> CidadesTemperatura { get; set; }

        public DbSet<Temperatura> Temperaturas { get; set; }
    }
}
