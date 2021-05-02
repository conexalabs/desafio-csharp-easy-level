using System;
using Application.Entidades.Base;

namespace Application.Interfaces.Repository.Base
{
    public interface IRepository<T> where T: EntityBase
    {
        T GetById(Guid id);
    }
}