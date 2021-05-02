using System;
using Application.Entidades.Base;
using Application.Interfaces.Repository.Base;

namespace Application.Interfaces.Service.Base
{
    public interface IBaseService<TViewModel, TEntity, TRepository> where TEntity : EntityBase where TRepository: IBaseRepository<TEntity>
    {
        TViewModel GetById(Guid id);

    }
}