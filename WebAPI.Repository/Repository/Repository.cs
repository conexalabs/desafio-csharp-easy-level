using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebAPI.Data.Context;
using WebAPI.Data.Interfaces;

namespace WebAPI.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApiContext _context = null;
        DbSet<T> _dbSet;
        public Repository(ApiContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> conditionExpression)
        {
            return _dbSet.FirstOrDefault(conditionExpression);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> conditionExpression)
        {
            if (conditionExpression != null)
                return _dbSet.Where(conditionExpression);

            return _dbSet.AsEnumerable();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
