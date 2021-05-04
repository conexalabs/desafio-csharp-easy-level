using System;
using System.Threading.Tasks;
using Application.Entidades.Base;
using Application.Interfaces.Repository.Base;
using Application.Interfaces.Service.Base;
using AutoMapper;

namespace Application.Services.Base
{
    public class BaseService<TViewModelRequest,TViewModelResponse, TEntity, TRepository> : IBaseService<TViewModelRequest,TViewModelResponse, TEntity> where TEntity : EntityBase
    {
        private TRepository _baseRepository;
        private IMapper _mapper;
        public BaseService(TRepository baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public TViewModelResponse GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update(TEntity t)
        {
            throw new NotImplementedException();
        }

        public TViewModelResponse Add(TEntity TEntity)
        {
            throw new NotImplementedException();
        }
    }
}