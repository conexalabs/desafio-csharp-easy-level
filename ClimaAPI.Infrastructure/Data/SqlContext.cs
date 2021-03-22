using System;
using System.Linq;
using ClimaAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClimaAPI.Infrastructure.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {
        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Registro> Registros { get; set; }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Data") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Data").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("Data").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }
}