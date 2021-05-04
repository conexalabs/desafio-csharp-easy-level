using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Entidades.Base;
using Application.Interfaces.Repository.Base;

namespace Application.Interfaces.Service.Base
{
    public interface IBaseService<TViewModelRequest,TViewModelResponse, TEntity> where TEntity : EntityBase
    {
        Task<TEntity> Update(TEntity t);
    }
}