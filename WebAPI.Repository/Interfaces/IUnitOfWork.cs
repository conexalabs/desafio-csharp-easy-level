using WebAPI.Domain.Models.Entities;

namespace WebAPI.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<CityWeather> CityWeatherRepository { get; }
        void Commit();
        void Rollback();
    }
}
