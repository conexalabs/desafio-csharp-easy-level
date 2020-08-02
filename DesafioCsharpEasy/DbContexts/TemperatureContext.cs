using Microsoft.EntityFrameworkCore;

namespace DesafioCsharpEasy.Models
{
    public class TemperatureContext : DbContext
    {
        public TemperatureContext(DbContextOptions<TemperatureContext> options)
            : base(options)
        { }
        
        public DbSet<Weather> Weathers { get; set; }
    }
}