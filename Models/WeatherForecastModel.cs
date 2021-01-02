using Microsoft.EntityFrameworkCore;

namespace desafio_csharp_easy_level.Models
{
    public class WeatherForecast
    {
        public string Name { get; set; }
        public double Temp { get; set; }
        public string Date { get; set; }
    }

    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}