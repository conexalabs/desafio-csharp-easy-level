using Microsoft.EntityFrameworkCore;

namespace desafioCsharpEasy.Models
{
    public class TemperatureContext : DbContext
    {
        public TemperatureContext(DbContextOptions<TemperatureContext> options)
            : base(options)
        { }
        
        public DbSet<Weather> Weathers { get; set; }
    }
}