using desafio_conexa.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_conexa.DbContexts
{
    public class TemperaturasDbContext : DbContext
    {
        public DbSet<Temperatura> Temperaturas { get; set; }
        public TemperaturasDbContext(DbContextOptions<TemperaturasDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
