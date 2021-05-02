using System;
using Application.Entidades.Base;
using Application.Interfaces.Repository.Base;
using Application.Interfaces.Service.Base;

namespace Application.Services.Base
{
    public class BaseService<TViewModel,TEntity, TRepository> : IBaseService<TViewModel, TEntity, TRepository> where TEntity : EntityBase where TRepository: IBaseRepository<TEntity>
    {
        public TViewModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}