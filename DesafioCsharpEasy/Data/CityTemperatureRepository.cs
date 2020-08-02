using DesafioCsharpEasy.Models;
using DesafioCsharpEasy.Repository;
using DesafioCsharpEasy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioCsharpEasy.Data
{
    public class CityTemperatureRepository : ICityTemperatureRepository
    {
        private readonly ApiContext _context;

        public CityTemperatureRepository(ApiContext apiContext)
        {
            _context = apiContext;
        }

        public CityTemperature GetById(int id)
        {
            return _context.CityTemperatures.FirstOrDefault(p => p.Id == id);
        }

        public CityTemperature GetByCity(string city)
        {
            return _context.CityTemperatures.FirstOrDefault(p => p.City == city);
        }

        public IEnumerable<CityTemperature> GetHistoricalLastMonth(string city, DateTime startDate, DateTime endDate)
        {
            var result = _context.CityTemperatures.Where(c => Text.RemoveAccents(c.City).ToLower() == city.ToLower() &&
                                                              c.DateTime >= startDate &&
                                                              c.DateTime <= endDate);

            return result;
        }

        public IEnumerable<CityTemperature> GetHistoricalLastMonth(string latutude, string longitude, DateTime startDate, DateTime endDate)
        {
            var result = _context.CityTemperatures.Where(c => c.Latitude.Replace(",", ".") == latutude.Replace(",", ".") &&
                                                              c.Longitude.Replace(",", ".") == longitude.Replace(",", ".") &&
                                                              c.DateTime >= startDate &&
                                                              c.DateTime <= endDate);

            return result;
        }

        public IEnumerable<CityTemperature> GetAll()
        {
            return _context.CityTemperatures.ToList();
        }

        public void Add(CityTemperature cityTemp)
        {
            if (cityTemp == null)
            {
                throw new ArgumentNullException(nameof(cityTemp));
            }

            _context.CityTemperatures.Add(cityTemp);
            _context.SaveChanges();
        }

        public void Update(CityTemperature cityTemp)
        {
            if (cityTemp == null)
            {
                throw new ArgumentNullException(nameof(cityTemp));
            }

            _context.CityTemperatures.Update(cityTemp);
            _context.SaveChanges();
        }

        public void Remove(CityTemperature cityTemp)
        {
            if (cityTemp == null)
            {
                throw new ArgumentNullException(nameof(cityTemp));
            }

            _context.CityTemperatures.Remove(cityTemp);
            _context.SaveChanges();
        }
    }
}
