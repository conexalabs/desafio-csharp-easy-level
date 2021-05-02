using System;
using Application.Entidades.Base;
using Application.Interfaces.Repository.Base;
using Application.Interfaces.Service.Base;
using AutoMapper;

namespace Application.Services.Base
{
    public class BaseService<TViewModel,TEntity, TRepository> : IBaseService<TViewModel, TEntity> where TEntity : EntityBase
    {
        private TRepository _baseRepository;
        private IMapper _mapper;
        public BaseService(TRepository baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public TViewModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public TViewModel Add(TEntity TEntity)
        {
            throw new NotImplementedException();
        }
    }
}