using Application.Entidades.City;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.DBContext
{
    public class ConexaContext : DbContext
    {
        public ConexaContext(DbContextOptions<ConexaContext> options) : base(options){}
        
        public DbSet<City> Citys { get; set; }        
    }
}