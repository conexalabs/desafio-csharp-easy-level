using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Data.DBContext
{
    public class ConexaDesignContextFactory : IDesignTimeDbContextFactory<ConexaContext>
    {
        public ConexaContext CreateDbContext(string[] args)
        {
            var optionsBuilde = new DbContextOptionsBuilder<ConexaContext>();
            optionsBuilde.UseSqlServer(
                @"Server=localhost,1433;Database=ConexaDB;User Id=SA;Password=0rUOw5M5ad;");
            return new ConexaContext(optionsBuilde.Options);
        }
    }
}