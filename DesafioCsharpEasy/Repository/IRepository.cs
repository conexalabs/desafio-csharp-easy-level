using System.Collections.Generic;

namespace DesafioCsharpEasy.Repository
{
    public interface IRepository<TModel> where TModel : class
    {
        TModel GetById(int id);
        IEnumerable<TModel> GetAll();
        void Add(TModel entity);
        void Update(TModel entity);
        void Remove(TModel entity);
    }
}
