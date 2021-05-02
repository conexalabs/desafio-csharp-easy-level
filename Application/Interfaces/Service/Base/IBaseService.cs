using System;
using System.Collections.Generic;
using Application.Entidades.Base;
using Application.Interfaces.Repository.Base;

namespace Application.Interfaces.Service.Base
{
    public interface IBaseService<TViewModel, TEntity> where TEntity : EntityBase
    {
        TViewModel GetById(Guid id);
    }
}