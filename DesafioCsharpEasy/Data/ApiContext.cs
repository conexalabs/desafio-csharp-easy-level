using Microsoft.EntityFrameworkCore;

namespace DesafioCsharpEasy.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<CityTemperature> CityTemperatures { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
    }
}
