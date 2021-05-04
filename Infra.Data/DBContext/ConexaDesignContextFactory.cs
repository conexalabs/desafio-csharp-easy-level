using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Data.DBContext
{
    public class ConexaDesignContextFactory : IDesignTimeDbContextFactory<ConexaContext>
    {
        public ConexaContext CreateDbContext(string[] args)
        {
            var optionsBuilde = new DbContextOptionsBuilder<ConexaContext>();
            optionsBuilde.UseInMemoryDatabase("ConexaDB");
            return new ConexaContext(optionsBuilde.Options);
        }
    }
}