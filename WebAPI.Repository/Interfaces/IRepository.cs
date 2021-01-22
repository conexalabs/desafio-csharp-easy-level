using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebAPI.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        T Get(Expression<Func<T, bool>> conditionExpression);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> conditionExpression);
    }
}
