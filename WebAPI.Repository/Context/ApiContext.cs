using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Data.Context
{
    public class ApiContext : DbContext
    {
        public virtual DbSet<CityWeather> CityWeathers { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
