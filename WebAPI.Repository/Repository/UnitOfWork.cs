using WebAPI.Data.Context;
using WebAPI.Data.Interfaces;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _context = null;
        private Repository<CityWeather> _cityWeatherRepository = null;

        public UnitOfWork(ApiContext context)
        {
            _context = context;
        }
        public IRepository<CityWeather> CityWeatherRepository
        {
            get
            {
                if(_cityWeatherRepository == null)
                {
                    _cityWeatherRepository = new Repository<CityWeather>(_context);
                }
                return _cityWeatherRepository;
            }
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            _context.Dispose();
        }
    }
}
