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
    }
}