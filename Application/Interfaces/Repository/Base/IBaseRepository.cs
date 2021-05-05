using System;
using System.Threading.Tasks;
using Application.Entidades.Base;

namespace Application.Interfaces.Repository.Base
{
    public interface IBaseRepository<T> where T: EntityBase
    {
        T GetById(Guid id);
        Task<T> Add(T t);
        Task<T> Update(T t);
    }
}