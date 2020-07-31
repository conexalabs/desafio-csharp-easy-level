using desafio_conexa.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_conexa.DbContexts
{
    public class CidadesDbContext : DbContext
    {
        public DbSet<Cidade> Cidades { get; set; }
        public CidadesDbContext(DbContextOptions<CidadesDbContext> options)
            : base(options)
        { }
    }
}
